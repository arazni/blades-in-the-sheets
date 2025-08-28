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
		output.Should().ContainAll
		(
			"\"ActionsByName\":",
			"\"AttributesByName\":",
			"\"Version\": 4",
			"\"Language\": \"English\"",
			"\"MaxBulkByCommitmentOption\"",
			"\"IsCommitmentLocked\": false"
		);
		this.serializer.Deserialize(output);
	}

	[Fact]
	public void Migrator_HandlesAlreadyMigrated()
	{
		var output = this.migrator.Migrate(JsonJunk.SpiderJsonV1);
		output = this.migrator.Migrate(output);
		this.serializer.Deserialize(output);
	}

	[Fact]
	public void Migrator_v4_FixesFrenchMissingBackground()
	{
		var output = this.migrator.Migrate(JsonJunk.FrenchMissingBackgroundV3);
		output.Should().Contain("\"Name\": \"Académique\"");
	}

	[Fact]
	public void Migrator_v4_IgnoresFrenchWithBackground()
	{
		var output = this.migrator.Migrate(JsonJunk.FrenchAlternateBackgroundV3("Commerce"));
		output.Should().Contain("\"Name\": \"Commerce\"");
	}

	[Fact]
	public void Migrator_v4_FixesEnglishMissingBackground()
	{
		var output = this.migrator.Migrate(JsonJunk.LazyEnglishAlternateBackgroundV3(""));
		output.Should().Contain("\"Name\": \"Academic\"");
	}

	[Fact]
	public void Migrator_v4_IgnoresEnglishWithBackground()
	{
		var output = this.migrator.Migrate(JsonJunk.LazyEnglishAlternateBackgroundV3("Underworld"));
		output.Should().Contain("\"Name\": \"Underworld\"");
	}
}
