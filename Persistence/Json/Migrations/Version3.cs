using Newtonsoft.Json.Linq;
using static Persistence.Json.Migrations.IMigrationHandler;
using static Persistence.Json.Migrations.VersionFunctions;

namespace Persistence.Json.Migrations;

internal static class Version3
{
	const int Version = 3;

	public static JsonVersion MigrateV3(this JsonVersion migration)
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
		AddProperty(jObject, "$.Gear", "IsCommitmentLocked", false);

		AddObject(jObject, "$.Gear.Commitment", "MaxBulkByCommitmentOption", new
		{
			None = 0,
			Light = 3,
			Normal = 5,
			Heavy = 6,
			Encumbered = 9
		});

		DefaultLanguage(jObject);

		ReplaceVersion(jObject, Version);

		return jObject;
	}

	private static JObject DefaultLanguage(JObject jObject)
	{
		var languagePath = jObject.SelectToken("$.Language");

		if (languagePath != null)
			return jObject;

		AddProperty(jObject, "$", "Language", Models.Settings.Constants.Languages.English);
		return jObject;
	}
}
