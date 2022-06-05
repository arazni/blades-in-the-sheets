using Domain.Characters;
using Newtonsoft.Json;

namespace Persistence.Json;

public class Serializer : ISerializer
{
	private readonly JsonSerializerSettings serializerSettings;

	public Serializer()
	{
		this.serializerSettings = new JsonSerializerSettings
		{
			Formatting = Formatting.Indented,
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			ContractResolver = new BladesJsonContractResolver()
		};
	}

	public string Serialize(Character character) =>
		JsonConvert.SerializeObject(character, this.serializerSettings);

	public Character Deserialize(string json) =>
		JsonConvert.DeserializeObject<Character>(json, this.serializerSettings)
		?? throw new ArgumentException("Could not deserialize: " + json, nameof(json));
}
