using Blazored.LocalStorage;
using Domain.Characters;
using Persistence.Json;

namespace UI.Services;

public class StorageService : IStorageService
{
	private readonly ILocalStorageService storageService;
	private readonly ISerializer serializer;

	public StorageService(ILocalStorageService localStorageService, ISerializer serializer)
	{
		this.storageService = localStorageService;
		this.serializer = serializer;
	}

	public async Task<string> Save(Character character)
	{
		var json = this.serializer.Serialize(character);
		await this.storageService.SetItemAsStringAsync(character.Id, json);

		return character.Id;
	}

	public async Task<Character> Load(string id)
	{
		var json = await this.storageService.GetItemAsStringAsync(id);

		return this.serializer.Deserialize(json);
	}

	public async Task<IReadOnlyCollection<Character>> LoadAll()
	{
		var all = await this.storageService.KeysAsync();

		var characterJsons = new List<string>(all.Count());

		foreach (var key in all)
			characterJsons.Add(await this.storageService.GetItemAsStringAsync(key));

		return characterJsons.Select(j => this.serializer.Deserialize(j))
			.ToArray();
	}
}
