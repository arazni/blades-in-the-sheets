namespace Persistence.Json.DataModels;

public record GameFile(string Name, string File);

public static class GameFileExtensions
{
	public static GameFile ToGameFile(string name) =>
		new
		(
			name,
			$"{name.Replace(" ", "-").ToLowerInvariant()}.json"
		);
}
