using FluentAssertions;
using Persistence.Json;
using Persistence.Json.Migrations;
using Xunit;

namespace Persistence.Test;

public class MigratorTests
{
	private readonly ISerializer serializer;
	private readonly IMigrationHandler migrator;
	public MigratorTests()
	{
		this.serializer = new Serializer();
		this.migrator = new MigrationHandler();
	}

	[Fact]
	public void Migrator_Migrates()
	{
		var output = this.migrator.Migrate(JsonJunk.SpiderJsonV1);
		output.Should().NotContainAny("\"Source\":", "\"PlaybookDefault\":");
		output.Should().ContainAll("\"ActionsByName\":", "\"AttributesByName\":", "\"Version\": 2");
		this.serializer.Deserialize(output);
	}

	[Fact]
	public void Migrator_HandlesAlreadyMigrated()
	{
		var output = this.migrator.Migrate(JsonJunk.SpiderJsonV1);
		output = this.migrator.Migrate(output);
		this.serializer.Deserialize(output);
	}
}
