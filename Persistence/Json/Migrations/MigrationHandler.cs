using Newtonsoft.Json.Linq;
using static Persistence.Json.Migrations.IMigrationHandler;

namespace Persistence.Json.Migrations;

public class MigrationHandler : IMigrationHandler
{
	public string Migrate(string json)
	{
		var jObject = JObject.Parse(json);

		int version;
		if (jObject.SelectToken("$.Version") is not JValue jVersion || !jVersion.HasValues)
			version = 1;
		else
			version = jVersion.Value<int>();

		JsonVersion jsonVersion = new
		(
			JObject.Parse(json),
			version
		);

		// Chain future migrations here
		var final = jsonVersion.MigrateV2();

		return final.Json.ToString();
	}

	//public VersionResult NeedsToMigrate(string fileName)
	//{
	//	var pieces = fileName.Split('.');

	//	if (pieces.IsNullOrEmpty() || !pieces.Last().Like("json"))
	//		throw new ApplicationException("Expecting the file name to end with .json");

	//	if (pieces.Length == 2)
	//		return new(1, true);

	//	string possibleVersion = pieces[^2];

	//	if (!possibleVersion.StartsWith("version"))
	//		//throw new ApplicationException("Expecting the file name to end with .version{number}.json, where {number} is the file version");
	//		return new(1, true);

	//	var versionText = possibleVersion["version".Length..];

	//	if (!int.TryParse(versionText, out var versionNumber))
	//		//throw new ApplicationException("Expecting the file name to end with .version{number}.json, where {number} is the file version");
	//		return new(1, true);

	//	if (versionNumber < MaxMigrationVersion)
	//		return new(versionNumber, true);

	//	return new(versionNumber, false);
	//}
}
