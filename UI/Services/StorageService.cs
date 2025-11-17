using Blazored.LocalStorage;
using Models.Characters;
using Models.Common;
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
		var json = await this.storageService.GetItemAsStringAsync(key)
		?? throw new FileNotFoundException($"Failed to find {CharacterKey(id)} in local storage");

		json = this.migrator.Migrate(json);

		return this.serializer.Deserialize(json);
	}

	private record JsonKey(string Json, string Key);
	public async IAsyncEnumerable<ErrorResult<Character, LoadError>> LoadAllAsResults()
	{
		var results = (await this.storageService.KeysAsync())
			.Where(key => key.StartsWith(CharacterPrefix + KeyDelimiter))
			// get
			.Select(async key => (key[(CharacterPrefix.Length + KeyDelimiter.Length)..], character: await this.storageService.GetItemAsStringAsync(key)))
			// migrate
			.Select(async keyCharacter =>
			{
				var (key, characterJson) = await keyCharacter;

				if (!characterJson.HasInk())
				{
					var error = new LoadError($"Failed to find any data for a character with key {key}. This should never happen and I'm not sure I can do anything about it if it does. Please do your best to remember and communicate what happened to this character before this error.", "I'm sorry, I've got no details.", characterJson, key);
					return new ErrorResult<JsonKey, LoadError>(error);
				}

				try
				{
					var migratedJson = this.migrator.Migrate(characterJson);
					return new ErrorResult<JsonKey, LoadError>(new JsonKey(migratedJson, key));
				}
				catch (Exception e)
				{
					var error = new LoadError($"Failed to migrate character with key {key}.", e.ExceptionDetails(), characterJson, key);
					return new ErrorResult<JsonKey, LoadError>(error);
				}
			})
			// deserialize
			.Select(async migratedResult => (await migratedResult).Switch
			(
				jsonKey =>
				{
					try
					{
						var character = this.serializer.Deserialize(jsonKey.Json);
						return new ErrorResult<Character, LoadError>(character);
					}
					catch (Exception e)
					{
						var error = new LoadError($"Failed to deserialize character with key {jsonKey.Key}.", e.ExceptionDetails(), jsonKey.Json, jsonKey.Key);
						return new ErrorResult<Character, LoadError>(error);
					}
				},
				error => new ErrorResult<Character, LoadError>(error)
			));

		// don't understand why i have to do this instead of .ToAsyncEnumerable()
		foreach (var result in results)
			yield return await result;
	}

	public async Task Delete(string id) =>
		await this.storageService.RemoveItemAsync(CharacterKey(id));

	public async Task<string> GetFile(string id)
	{
		var json = await this.storageService.GetItemAsStringAsync(CharacterKey(id))
		?? throw new FileNotFoundException($"Failed to find {CharacterKey(id)} in local storage");

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
