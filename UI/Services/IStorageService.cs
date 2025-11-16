using Models.Characters;
using Models.Common;

namespace UI.Services;

public interface IStorageService
{
	Task<Character> Load(string id);
	Task<string> Save(Character character);
	Task Delete(string id);
	Task<string> GetFile(string id);
	Task PutFile(string json);
	IAsyncEnumerable<ErrorResult<Character, LoadError>> LoadAllAsResults();
}