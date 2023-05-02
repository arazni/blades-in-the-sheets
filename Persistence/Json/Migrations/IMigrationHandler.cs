using Newtonsoft.Json.Linq;

namespace Persistence.Json.Migrations;

public interface IMigrationHandler
{
	string Migrate(string json);

	record VersionResult(int Version, bool NeedsMigration);

	record JsonVersion(JObject Json, int Version);
}
