using Domain.Characters;
using FluentAssertions;
using Newtonsoft.Json;
using Persistence.Json;
using System;
using Xunit;

namespace Persistence.Test;

public class SerializerTests
{
	private readonly ISerializer serializer;

	public SerializerTests()
	{
		this.serializer = new Serializer();
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
	public (string, string) Serializer_Serializes_LurkCharacter()
	{
		var character = new Character(PlaybookOption.Lurk);
		var id = character.Id;
		var json = this.serializer.Serialize(character);
		json.Should().NotBeNullOrWhiteSpace();
		return (id, json);
	}

	[Fact]
	public void Serializer_Deserializes_RealCharacter()
	{
		var character = this.serializer.Deserialize(JsonJunk.SpiderJson);
		character.Should().NotBeNull();
		character.Playbook.Option.Should().Be(PlaybookOption.Spider);
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
	""Id"": ""15bb5abd-09d0-472e-98db-ae65227889c0"",
	""Dossier"": {
		""Name"": ""Eleanor"",
		""CrewId"": """",
		""Alias"": ""Weave"",
		""Look"": ""Weathered"",
		""Notes"": """",
		""Background"": {
			""Background"": 2,
			""Description"": """"
		},
		""Heritage"": {
			""Heritage"": 1,
			""Description"": """"
		},
		""Vice"": {
			""Vice"": 4,
			""Description"": """"
		}
	},
	""Monitor"": {
		""Stress"": {
			""CurrentStress"": 0
		},
		""Trauma"": {},
		""Harm"": {},
		""Armor"": {}
	},
	""Talent"": {
		""Insight"": {
			""Hunt"": {
				""Rating"": 0,
				""PlaybookDefault"": 0
			},
			""Study"": {
				""Rating"": 1,
				""PlaybookDefault"": 1
			},
			""Survey"": {
				""Rating"": 1,
				""PlaybookDefault"": 0
			},
			""Tinker"": {
				""Rating"": 0,
				""PlaybookDefault"": 0
			}
		},
		""Prowess"": {
			""Finesse"": {
				""Rating"": 1,
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
			}
		}
	},
	""Playbook"": {
		""Experience"": {
			""Experience"": 0
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