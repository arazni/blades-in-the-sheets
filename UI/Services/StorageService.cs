using Blazored.LocalStorage;
using Models.Characters;
using Persistence.Json;
using Persistence.Json.Migrations;

namespace UI.Services;

public class StorageService : IStorageService
{
	private readonly ILocalStorageService storageService;
	private readonly ISerializer serializer;
	private readonly IMigrationHandler migrator;
	private const string CharacterPrefix = "character";
	private const string KeyDelimiter = "/";

	public StorageService(ILocalStorageService localStorageService, ISerializer serializer, IMigrationHandler migrator)
	{
		this.storageService = localStorageService;
		this.serializer = serializer;
		this.migrator = migrator;
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

		json = this.migrator.Migrate(json);

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

		return characterJsons
			.Select(this.migrator.Migrate)
			.Select(this.serializer.Deserialize)
			.ToArray();
	}

	public async Task Delete(string id) =>
		await this.storageService.RemoveItemAsync(CharacterKey(id));

	public async Task<string> GetFile(string id)
	{
		var json = await this.storageService.GetItemAsStringAsync(CharacterKey(id));

		return this.migrator.Migrate(json);
	}

	public async Task PutFile(string json)
	{
		var migratedJson = this.migrator.Migrate(json);

		var character = this.serializer.Deserialize(migratedJson);

		if (character == null) return;

		await Save(character);
	}

	private static string CharacterKey(string characterId) =>
		$"{CharacterPrefix}{KeyDelimiter}{characterId}";

	private static string Key(Character character) =>
		CharacterKey(character.Id.ToString());
}
