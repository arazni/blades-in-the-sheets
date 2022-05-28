using Domain.Characters;
using Newtonsoft.Json;

namespace Persistence.Json;

public class Saver : ISaver
{
	public const string CharacterFileName = "user.characters";

	public async Task Save(Character character)
	{
		var data = JsonConvert.SerializeObject(character, Formatting.Indented);

		await WriteFile(CharacterFileName, character.Id, data);
	}

	private static async Task WriteFile(string prefix, Guid id, string data) =>
		await File.WriteAllTextAsync($"{prefix}.{id}.json", data);
}
