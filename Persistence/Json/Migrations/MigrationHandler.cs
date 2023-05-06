using Newtonsoft.Json.Linq;
using static Persistence.Json.Migrations.IMigrationHandler;

namespace Persistence.Json.Migrations;

public class MigrationHandler : IMigrationHandler
{
	public string Migrate(string json)
	{
		var jObject = JObject.Parse(json);

		int version;
		if (jObject.SelectToken("$.Version") is not JValue jVersion)
			version = 1;
		else
			version = jVersion.Value<int>();

		if (version == MaxVersion)
			return json;

		JsonVersion jsonVersion = new
		(
			JObject.Parse(json),
			version
		);

		// Chain future migrations here
		var final = jsonVersion.MigrateV2();

		return final.Json.ToString();
	}
}
