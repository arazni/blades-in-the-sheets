using Models.Common;
using Models.Settings;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Models.Legacy.RetiredOptions;
using static Persistence.Json.Migrations.IMigrationHandler;
using static Persistence.Json.Migrations.VersionFunctions;

namespace Persistence.Json.Migrations;

internal static class Version2
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
		ReplaceDossierChildName(jObject, "Heritage", (i) => ((HeritageOption)i).Description());
		ReplaceDossierChildName(jObject, "Vice", (i) => ((ViceOption)i).ToString());

		NestProperty(jObject, "$.Talent", "AttributesByName");
		NestProperty(jObject, "$.Talent..Insight", "ActionsByName", "Experience");
		NestProperty(jObject, "$.Talent..Prowess", "ActionsByName", "Experience");
		NestProperty(jObject, "$.Talent..Resolve", "ActionsByName", "Experience");

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
}
