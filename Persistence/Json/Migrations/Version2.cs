using Models.Common;
using Models.Settings;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Models.Legacy.RetiredOptions;
using static Persistence.Json.Migrations.IMigrationHandler;

namespace Persistence.Json.Migrations;

public static class Version2
{
	public static JsonVersion MigrateV2(this JsonVersion migration)
	{
		if (migration.Version > 1)
			return migration;

		return new
		(
			Upgrade(migration.Json),
			migration.Version + 1
		);
	}

	public static JObject Upgrade(JObject jObject)
	{
		RemoveSources(jObject);

		ReplacePlaybookOption(jObject);
		ReplaceTraumaOption(jObject);
		ReplacePlaybookDefaultsWithMaxRatings(jObject);

		ReplaceDossierChildName(jObject, "Background", (i) => ((BackgroundOption)i).ToString());
		ReplaceDossierChildName(jObject, "Heritage", (i) => ((HeritageOption)i).ToString());
		ReplaceDossierChildName(jObject, "Vice", (i) => ((ViceOption)i).ToString());

		NestProperty(jObject, "$.Talent", "AttributesByName");
		NestProperty(jObject, "$.Talent..Insight", "ActionsByName");
		NestProperty(jObject, "$.Talent..Prowess", "ActionsByName");
		NestProperty(jObject, "$.Talent..Resolve", "ActionsByName");

		AddObject(jObject, "$.Gear.AvailableGearByName", "A Coin", new { Bulk = 1, Name = "A Coin" });
		AddObject(jObject, "$.Gear.AvailableGearByName", "2 Coin", new { Bulk = 2, Name = "2 Coin" });
		AddObject(jObject, "$.Gear.AvailableGearByName", "3 Coin", new { Bulk = 3, Name = "3 Coin" });
		AddObject(jObject, "$.Gear.AvailableGearByName", "4 Coin", new { Bulk = 4, Name = "4 Coin" });

		AddProperty(jObject, "$", "GameName", Constants.Games.BladesInTheDark);
		AddProperty(jObject, "$", "Version", 2);
		AddProperty(jObject, "$.Monitor.Harm.HealingClock", "Size", 4);

		return jObject;
	}

	private static void ReplacePlaybookOption(JObject json)
	{
		const string path = "$.Playbook.Option";

		var optionProperty = json.SelectToken(path)?.Parent as JProperty
			?? throw Error(path);

		var option = (PlaybookOption)optionProperty.Value.Value<int>();
		var nameProperty = new JProperty("Name", option.ToString());

		optionProperty.Replace(nameProperty);
	}

	private static void ReplacePlaybookDefaultsWithMaxRatings(JObject json)
	{
		const string path = "$.Talent..PlaybookDefault";

		var playbookDefaults = json.SelectTokens(path)
			.Select(pd => pd.Parent!)
			.ToArray();

		if (playbookDefaults.IsNullOrEmpty())
			throw Error(path);

		foreach (var property in playbookDefaults)
			property.Replace(new JProperty("MaxRating", 4));
	}

	private static void ReplaceDossierChildName(JObject json, string propertyName, Func<int, string> optionIntToName)
	{
		var path = $"$.Dossier.{propertyName}.{propertyName}";

		var dossierProperty = json.SelectToken(path)?.Parent as JProperty
			?? throw Error(path);

		var value = optionIntToName(dossierProperty.Value.Value<int>());
		dossierProperty.Replace(new JProperty("Name", value));
	}

	private static void RemoveSources(JObject json)
	{
		const string path = "$..Source";

		var allSources = json.SelectTokens(path)
			.Select(t => t.Parent!)
			.ToArray();

		if (allSources.IsNullOrEmpty())
			throw Error(path);

		foreach (var property in allSources)
			property.Remove();
	}

	private static void NestProperty(JObject json, string path, string childPropertyName)
	{
		var parentProperty = json.SelectToken(path) as JObject
			?? throw Error(path);

		var children = parentProperty.DeepClone();

		foreach (var child in parentProperty.Children().ToArray())
			child.Remove();

		parentProperty.Add(childPropertyName, children);
	}

	private static void AddObject<T>(JObject json, string path, string objectName, T obj) where T : notnull
	{
		var parentProperty = json.SelectToken(path) as JObject
			?? throw Error(path);

		var addable = JObject.FromObject(obj);
		parentProperty.Add(objectName, addable);
	}

	private static void AddProperty<T>(JObject json, string path, string propertyName, T propertyValue)
	{
		var parentProperty = json.SelectToken(path) as JObject
			?? throw Error(path);

		parentProperty.Add(propertyName, new JValue(propertyValue));
	}

	private static void ReplaceTraumaOption(JObject json)
	{
		var optionProperty = json.SelectToken("$.Monitor.Trauma.TraumaSet") as JArray
			?? throw new JsonException("Cannot migrate to v2. Cannot find JPATH $.Monitor.Trauma.TraumaSet");

		var children = optionProperty.Children()
			.Select(c => ((TraumaOption)c.Value<int>()).ToString())
			.ToArray();

		optionProperty.RemoveAll();

		foreach (var child in children)
			optionProperty.Add(child);
	}

	private static JsonException Error(string jPath) =>
		new($"Cannot migrate to v2. Cannot find JPATH {jPath}");
}
