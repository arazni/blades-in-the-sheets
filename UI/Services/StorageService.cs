using Blazored.LocalStorage;
using Domain.Characters;
using Persistence.Json;

namespace UI.Services;

public class StorageService : IStorageService
{
	private readonly ILocalStorageService storageService;
	private readonly ISerializer serializer;
	private const string CharacterPrefix = "character";
	private const string KeyDelimiter = "/";

	public StorageService(ILocalStorageService localStorageService, ISerializer serializer)
	{
		this.storageService = localStorageService;
		this.serializer = serializer;
	}

	public async Task<string> Save(Character character)
	{
		var json = this.serializer.Serialize(character);
		var key = Key(character);

		await this.storageService.SetItemAsStringAsync(key, json);

		return character.Id;
	}

	public async Task<Character> Load(string id)
	{
		var key = CharacterKey(id);
		var json = await this.storageService.GetItemAsStringAsync(key);

		return this.serializer.Deserialize(json);
	}

	public async Task<IReadOnlyCollection<Character>> LoadAll()
	{
		var all = (await this.storageService.KeysAsync())
			.Where(key => key.StartsWith(CharacterPrefix + KeyDelimiter))
			.ToArray();

		var characterJsons = new List<string>(all.Length);

		foreach (var key in all)
			characterJsons.Add(await this.storageService.GetItemAsStringAsync(key));

		return characterJsons.Select(j => this.serializer.Deserialize(j))
			.ToArray();
	}

	private static string CharacterKey(string characterId) =>
		$"{CharacterPrefix}{KeyDelimiter}{characterId}";

	private static string Key(Character character) =>
		CharacterKey(character.Id.ToString());
}
