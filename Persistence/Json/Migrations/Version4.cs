using Models.Common;
using Newtonsoft.Json.Linq;
using static Persistence.Json.Migrations.IMigrationHandler;
using static Persistence.Json.Migrations.VersionFunctions;

namespace Persistence.Json.Migrations;
internal static class Version4
{
	const int Version = 4;

	public static JsonVersion MigrateV4(this JsonVersion migration)
	{
		if (migration.Version >= Version)
			return migration;

		return new
		(
			Upgrade(migration.Json),
			Version
		);
	}

	public static JObject Upgrade(JObject jObject)
	{
		var language = jObject.SelectToken("$.Language")?.Value<string>()
				?? throw Error("$.Language");

		string replacement = "Academic";

		if (language.Like("Français"))
			replacement = "Académique";

		DefaultPropertyValue(jObject, "$.Dossier.Background", "Name", replacement);

		ReplaceVersion(jObject, Version);

		return jObject;
	}
}
