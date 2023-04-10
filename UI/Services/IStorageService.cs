using Models.Characters;

namespace UI.Services;

public interface IStorageService
{
	Task<Character> Load(string id);
	Task<IReadOnlyCollection<Character>> LoadAll();
	Task<string> Save(Character character);
	Task Delete(string id);
	Task<string> GetFile(string id);
	Task PutFile(string json);
}