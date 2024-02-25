﻿using Models.Characters;
using Models.Settings;
using Newtonsoft.Json;
using Persistence.Json.Converters;

namespace Persistence.Json;

public class Serializer : ISerializer
{
	private readonly JsonSerializerSettings serializerSettings;

	public Serializer()
	{
		this.serializerSettings = new JsonSerializerSettings
		{
			Formatting = Formatting.Indented,
			ObjectCreationHandling = ObjectCreationHandling.Reuse, // preserve objects such as Dictionary with StringComparer
			ContractResolver = new BladesJsonContractResolver(),
			Converters = new List<JsonConverter>()
			{
				new ExperienceTrackerConverter(),
				new BoundedCollectionStringConverter(),
				new RolloverClockConverter()
			}
		};
	}

	public string Serialize(Character character) =>
		JsonConvert.SerializeObject(character, this.serializerSettings);

	public Character Deserialize(string json) =>
		JsonConvert.DeserializeObject<Character>(json, this.serializerSettings)
		?? throw new ArgumentException("Could not deserialize: " + json, nameof(json));

	public string Serialize(ThemeSetting themeSetting) =>
		JsonConvert.SerializeObject(themeSetting, this.serializerSettings);

	public ThemeSetting DeserializeTheme(string json) =>
		JsonConvert.DeserializeObject<ThemeSetting>(json, this.serializerSettings)
		?? throw new ArgumentException("Could not deserialize: " + json, nameof(json));

	public string Serialize(AccessibilitySetting accessibilitySetting) =>
		JsonConvert.SerializeObject(accessibilitySetting, this.serializerSettings);

	public AccessibilitySetting DeserializeAccessibility(string json) =>
		JsonConvert.DeserializeObject<AccessibilitySetting>(json, this.serializerSettings)
		?? throw new ArgumentException("Could not deserialize: " + json, nameof(json));
}
