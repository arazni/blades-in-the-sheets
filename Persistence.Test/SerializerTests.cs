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