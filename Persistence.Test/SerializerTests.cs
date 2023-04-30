﻿using FluentAssertions;
using Models.Characters;
using Models.Common;
using Models.Settings;
using Newtonsoft.Json;
using Persistence.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static Models.Legacy.RetiredOptions;

namespace Persistence.Test;

public class SerializerTests
{
	private readonly ISerializer serializer;
	private readonly ILoader loader;
	private readonly ICharacterCoordinator characterCoordinator;

	public SerializerTests()
	{
		this.serializer = new Serializer();
		this.loader = new Loader(new ServerFileReader());
		this.characterCoordinator = new CharacterCoordinator(this.loader);
	}

	[Fact]
	public async Task<(string, string)> Serializer_Serializes_EmptyCharacter()
	{
		var character = await this.characterCoordinator.InitializeCharacter(Constants.Games.BladesInTheDark, PlaybookOption.Lurk.ToString());
		var id = character.Id;
		var json = this.serializer.Serialize(character);
		json.Should().NotBeNullOrWhiteSpace();
		return (id, json);
	}

	[Fact]
	public async Task Serializer_Deserializes_EmptyCharacter()
	{
		var (id, json) = await Serializer_Serializes_EmptyCharacter();

		var character = this.serializer.Deserialize(json);
		character.Should().NotBeNull();
		character.Playbook.Name.Should().Be(PlaybookOption.Lurk.ToString());
		character.Talent.ActionsByName[ActionName.Prowl.ToString()].Rating.Should().Be(2);
		character.Talent.ActionsByName[ActionName.Finesse.ToString()].Rating.Should().Be(1);
		character.Id.Should().Be(id);
	}

	[Fact]
	public async void Serializer_Serializes_CoordinatorCharacter()
	{
		var model = await this.characterCoordinator.InitializeCharacter(Constants.Games.BladesInTheDark, PlaybookOption.Leech.ToString());
		model.Should().NotBeNull();
		model.Playbook.Name.Should().Be(PlaybookOption.Leech.ToString());
		model.Gear.AvailableGear.Should().NotBeEmpty();
		model.Gear.AvailableGear.Should().Contain(g => g.Name.Like("Fine tinkering tools"));
		model.Gear.AvailableGear.Should().Contain(g => g.Name.Like("A Pistol"));

		var json = this.serializer.Serialize(model);
		var character = this.serializer.Deserialize(json);

		character.Should().NotBeNull();
		character.Playbook.Name.Should().Be(PlaybookOption.Leech.ToString());
		character.Gear.AvailableGear.Should().NotBeEmpty();
		character.Gear.AvailableGear.Should().Contain(g => g.Name.Like("Fine tinkering tools"));
		character.Gear.AvailableGear.Should().Contain(g => g.Name.Like("A Pistol"));
	}

	[Fact]
	public async void Serializer_Serializes_Experience()
	{
		var model = await this.characterCoordinator.InitializeCharacter(Constants.Games.BladesInTheDark, PlaybookOption.Leech.ToString());
		model.Should().NotBeNull();
		model.Playbook.Experience.Points = 5;
		model.Talent.AttributesByName[AttributeName.Resolve.ToString()].Experience.Points = 1;
		model.Talent.AttributesByName[AttributeName.Prowess.ToString()].Experience.Points = 3;
		model.Talent.AttributesByName[AttributeName.Insight.ToString()].Experience.Points = 2;

		var json = this.serializer.Serialize(model);
		json.Should().Contain("\"Points\": 5");
		var character = this.serializer.Deserialize(json);

		character.Should().NotBeNull();
		character.Playbook.Experience.Points.Should().Be(5);
		character.Talent.AttributesByName[AttributeName.Resolve.ToString()].Experience.Points.Should().Be(1);
		character.Talent.AttributesByName[AttributeName.Prowess.ToString()].Experience.Points.Should().Be(3);
		character.Talent.AttributesByName[AttributeName.Insight.ToString()].Experience.Points.Should().Be(2);

		character.Talent.AttributesByName[AttributeName.Resolve.ToString()].Experience.MaxPoints.Should().Be(6);
		character.Playbook.Experience.MaxPoints.Should().Be(8);
	}

	[Fact]
	public async void Serializer_Serializes_MonitorHarm()
	{
		var model = await this.characterCoordinator.InitializeCharacter(Constants.Games.BladesInTheDark, PlaybookOption.Leech.ToString());
		model.Should().NotBeNull();
		model.Monitor.Harm.AddHarm("lesser", HarmIntensity.Lesser);
		model.Monitor.Harm.AddHarm("lesser 2", HarmIntensity.Lesser);
		//model.Monitor.Harm.AddHarm("moderate", HarmIntensity.Moderate);
		model.Monitor.Harm.AddHarm("major", HarmIntensity.Severe);
		model.Monitor.Harm.AddHarm("oh no", HarmIntensity.Severe);
		model.IsDeadish.Should().BeTrue();

		var json = this.serializer.Serialize(model);
		json.Should().Contain("\"Items\":");
		var character = this.serializer.Deserialize(json);
		character.Should().NotBeNull();

		character.Monitor.Harm.LesserHarms.Should().HaveCount(2);
		//character.Monitor.Harm.ModerateHarms.Should().HaveCount(1);
		character.Monitor.Harm.SevereHarms.Should().HaveCount(1);
		character.Monitor.Harm.FatalHarms.Should().HaveCount(1);
		character.IsDeadish.Should().BeTrue();
	}

	[Fact]
	public async void Serializer_Serializes_RolloverClock()
	{
		var model = await this.characterCoordinator.InitializeCharacter(Constants.Games.BladesInTheDark, PlaybookOption.Leech.ToString());
		model.Should().NotBeNull();
		model.Monitor.Harm.HealingClock.Progress(5);
		model.Monitor.Harm.HealingClock.Time.Should().Be(4);
		model.Monitor.Harm.HealingClock.Rollover.Should().Be(1);

		var json = this.serializer.Serialize(model);
		json.Should().Contain("\"Rollover\": 1");
		json.Should().Contain("\"Time\": 4");

		var character = this.serializer.Deserialize(json);
		character.Should().NotBeNull();
		character.Monitor.Harm.HealingClock.Time.Should().Be(4);
		character.Monitor.Harm.HealingClock.Rollover.Should().Be(1);
	}

	[Fact]
	public async Task<(string, string)> Serializer_Serializes_LurkCharacter()
	{
		var character = await this.characterCoordinator.InitializeCharacter(Constants.Games.BladesInTheDark, PlaybookOption.Leech.ToString());
		character.Playbook.TakeAbility(new PlaybookSpecialAbility("Ambush", "When you attack from hiding or spring a trap, you get +1d.", 1));

		character.Rolodex.ReplaceFriends(new[] { new RolodexFriend("Favorite"), new RolodexFriend("Rival"), new RolodexFriend("3") });
		character.Rolodex.AssignOnlyCloseFriend(character.Rolodex.Friends.First(f => f.Entry == "Favorite"));
		character.Rolodex.AssignOnlyRival(character.Rolodex.Friends.First(f => f.Entry == "Rival"));

		var id = character.Id;
		var json = this.serializer.Serialize(character);
		json.Should().NotBeNullOrWhiteSpace();
		return (id, json);
	}

	[Fact]
	public async Task Serializer_Deserializes_LurkCharacter()
	{
		var (_, json) = await Serializer_Serializes_LurkCharacter();
		json.Should().NotBeNullOrWhiteSpace();

		var character = this.serializer.Deserialize(json);

		character.Should().NotBeNull();
		character.Playbook.Name.Should().Be(PlaybookOption.Lurk.ToString());
		character.Playbook.Abilities.Should().HaveCount(1);
		character.Playbook.Experience.MaxPoints.Should().BeGreaterThan(0);
		character.Talent.AttributesByName[AttributeName.Insight.ToString()].Experience.MaxPoints.Should().BeGreaterThan(0);
		character.Rolodex.Friends.Should().HaveCountGreaterThan(0);
		character.Rolodex.CloseFriend.Should().NotBeNull();
		character.Rolodex.Rival.Should().NotBeNull();
	}

	[Fact]
	public void Serializer_Deserializes_RealCharacter()
	{
		var character = this.serializer.Deserialize(JsonJunk.SpiderJson);
		character.Should().NotBeNull();
		character.Playbook.Name.Should().Be(PlaybookOption.Spider.ToString());
		character.Talent.AttributesByName[AttributeName.Insight.ToString()].Experience.MaxPoints.Should().BeGreaterThan(0);
	}

	[Fact]
	public void Wtf()
	{
		var raw = "{\"SomeField\":{\"One\":10,\"Id\":\"SerializedId\",\"NoSetterButInConstructor\":\"JsonValue\",\"PassedInConstructor\":2}}";
		var result = JsonConvert.DeserializeObject<ContainerClass>(raw, new JsonSerializerSettings
		{
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			ContractResolver = new BladesJsonContractResolver()
		}) ?? throw new Exception();

		result.SomeField.One.Should().Be(10);
		result.SomeField.NoSetterButInConstructor.Should().Be("SetByConstructorValue");
		result.SomeField.PassedInConstructor.Should().Be(2);
		result.SomeField.Id.Should().Be("SerializedId");
	}
}

public class FieldClass
{
	public FieldClass(int one, int passedInConstructor)
	{
		One = one;
		NoSetterButInConstructor = "SetByConstructorValue";
		PassedInConstructor = passedInConstructor;
	}

	public int One { get; } // <-- readonly property

	public int PassedInConstructor { get; } = 10;

	public string NoSetterButInConstructor { get; } = "OutsideConstructorValue";

	public string Id { get; private set; } = "DefaultId";
}

public class ContainerClass
{
	public FieldClass SomeField { get; set; } = new FieldClass(0, 100);  // <-- default value here
}

public class JsonJunk
{
	public const string SpiderJson = @"
{
  ""Id"": ""e8b8e79a-7823-4adf-9716-d5e7312c76a4"",
  ""Dossier"": {
    ""Name"": ""Billina"",
    ""CrewId"": """",
    ""Alias"": ""Thorn"",
    ""Look"": ""Brooding bookworm"",
    ""Notes"": """",
    ""Background"": {
      ""Background"": 1,
      ""Description"": ""I have to know everything""
    },
    ""Heritage"": {
      ""Heritage"": 3,
      ""Description"": ""Strange stuff happens here""
    },
    ""Vice"": {
      ""Vice"": 3,
      ""Description"": ""Harvale Brogan, the Centuralia Club, Brightstone.""
    }
  },
  ""Monitor"": {
    ""Stress"": {
      ""CurrentStress"": 0
    },
    ""Trauma"": {},
    ""Harm"": {}
  },
  ""Talent"": {
    ""Insight"": {
      ""Hunt"": {
        ""Rating"": 0,
        ""PlaybookDefault"": 0
      },
      ""Study"": {
        ""Rating"": 2,
        ""PlaybookDefault"": 1
      },
      ""Survey"": {
        ""Rating"": 1,
        ""PlaybookDefault"": 0
      },
      ""Tinker"": {
        ""Rating"": 0,
        ""PlaybookDefault"": 0
      },
      ""Experience"": {
        ""Points"": 0,
				""MaxPoints"": 6
      }
    },
    ""Prowess"": {
      ""Finesse"": {
        ""Rating"": 0,
        ""PlaybookDefault"": 0
      },
      ""Prowl"": {
        ""Rating"": 1,
        ""PlaybookDefault"": 0
      },
      ""Skirmish"": {
        ""Rating"": 0,
        ""PlaybookDefault"": 0
      },
      ""Wreck"": {
        ""Rating"": 0,
        ""PlaybookDefault"": 0
      },
      ""Experience"": {
        ""Points"": 0,
				""MaxPoints"": 6
      }
    },
    ""Resolve"": {
      ""Attune"": {
        ""Rating"": 0,
        ""PlaybookDefault"": 0
      },
      ""Command"": {
        ""Rating"": 0,
        ""PlaybookDefault"": 0
      },
      ""Consort"": {
        ""Rating"": 2,
        ""PlaybookDefault"": 2
      },
      ""Sway"": {
        ""Rating"": 1,
        ""PlaybookDefault"": 0
      },
      ""Experience"": {
        ""Points"": 0,
				""MaxPoints"": 6
      }
    }
  },
  ""Playbook"": {
    ""AbilitiesByName"": {
      ""Connected"": {
        ""Name"": ""Connected"",
        ""Description"": ""During downtime, you get +1 result level when you acquire an asset or reduce heat."",
        ""TimesTaken"": 0,
        ""Source"": 6
      }
    },
    ""Experience"": {
      ""Points"": 0,
			""MaxPoints"": 6
    },
    ""Option"": 6
  },
  ""Gear"": {
    ""Commitment"": {}
  },
  ""Fund"": {
    ""Satchel"": {
      ""Coins"": 0
    },
    ""Stash"": {
      ""Stash"": 0
    }
  },
  ""Rolodex"": {}
}
";
}