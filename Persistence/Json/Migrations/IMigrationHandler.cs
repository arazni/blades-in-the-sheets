using Newtonsoft.Json.Linq;

namespace Persistence.Json.Migrations;

public interface IMigrationHandler
{
	const int MaxVersion = 3;

	string Migrate(string json);

	record VersionResult(int Version, bool NeedsMigration);

	record JsonVersion(JObject Json, int Version);
}
