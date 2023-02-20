using FluentAssertions;
using Models.Characters;
using Newtonsoft.Json;
using Persistence.Json;
using System;
using System.Linq;
using Xunit;

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
	public (string, string) Serializer_Serializes_EmptyCharacter()
	{
		var character = new Character(PlaybookOption.Lurk);
		var id = character.Id;
		var json = this.serializer.Serialize(character);
		json.Should().NotBeNullOrWhiteSpace();
		return (id, json);
	}

	[Fact]
	public void Serializer_Deserializes_EmptyCharacter()
	{
		var (id, json) = Serializer_Serializes_EmptyCharacter();

		var character = this.serializer.Deserialize(json);
		character.Should().NotBeNull();
		character.Playbook.Option.Should().Be(PlaybookOption.Lurk);
		character.Talent.Prowl.Rating.Should().Be(2);
		character.Talent.Prowl.PlaybookDefault.Should().Be(2);
		character.Talent.Finesse.PlaybookDefault.Should().Be(1);
		character.Id.Should().Be(id);
	}

	[Fact]
	public async void Serializer_Serializes_CoordinatorCharacter()
	{
		var model = await this.characterCoordinator.InitializeCharacter(PlaybookOption.Leech);
		model.Should().NotBeNull();
		model.Playbook.Option.Should().Be(PlaybookOption.Leech);
		model.Gear.AvailableGear.Should().NotBeEmpty();
		model.Gear.AvailableGear.Where(g => g.Source == GearItem.Sources.Leech).Should().NotBeEmpty();
		model.Gear.AvailableGear.Where(g => g.Source == GearItem.Sources.Standard).Should().NotBeEmpty();

		var json = this.serializer.Serialize(model);
		var character = this.serializer.Deserialize(json);

		character.Should().NotBeNull();
		character.Playbook.Option.Should().Be(PlaybookOption.Leech);
		character.Gear.AvailableGear.Should().NotBeEmpty();
		character.Gear.AvailableGear.Where(g => g.Source == GearItem.Sources.Leech).Should().NotBeEmpty();
		character.Gear.AvailableGear.Where(g => g.Source == GearItem.Sources.Standard).Should().NotBeEmpty();
	}

	[Fact]
	public (string, string) Serializer_Serializes_LurkCharacter()
	{
		var character = new Character(PlaybookOption.Lurk);
		character.Playbook.TakeAbility(new PlaybookSpecialAbility("Ambush", "When you attack from hiding or spring a trap, you get +1d.", 1, PlaybookOption.Lurk));

		character.Rolodex.ReplaceFriends(new[] { new RolodexFriend("Favorite"), new RolodexFriend("Rival"), new RolodexFriend("3") });
		character.Rolodex.AssignOnlyCloseFriend(character.Rolodex.Friends.First(f => f.Entry == "Favorite"));
		character.Rolodex.AssignOnlyRival(character.Rolodex.Friends.First(f => f.Entry == "Rival"));

		var id = character.Id;
		var json = this.serializer.Serialize(character);
		json.Should().NotBeNullOrWhiteSpace();
		return (id, json);
	}

	[Fact]
	public void Serializer_Deserializes_LurkCharacter()
	{
		var (_, json) = Serializer_Serializes_LurkCharacter();
		json.Should().NotBeNullOrWhiteSpace();

		var character = this.serializer.Deserialize(json);

		character.Should().NotBeNull();
		character.Playbook.Option.Should().Be(PlaybookOption.Lurk);
		character.Playbook.Abilities.Should().HaveCount(1);
		character.Playbook.Experience.MaxPoints.Should().BeGreaterThan(0);
		character.Talent.Insight.Experience.MaxPoints.Should().BeGreaterThan(0);
		character.Rolodex.Friends.Should().HaveCountGreaterThan(0);
		character.Rolodex.CloseFriend.Should().NotBeNull();
		character.Rolodex.Rival.Should().NotBeNull();
	}

	[Fact]
	public void Serializer_Deserializes_RealCharacter()
	{
		var character = this.serializer.Deserialize(JsonJunk.SpiderJson);
		character.Should().NotBeNull();
		character.Playbook.Option.Should().Be(PlaybookOption.Spider);
		character.Talent.Insight.Experience.MaxPoints.Should().BeGreaterThan(0);
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
        ""Points"": 0
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
        ""Points"": 0
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
        ""Points"": 0
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
      ""Points"": 0
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