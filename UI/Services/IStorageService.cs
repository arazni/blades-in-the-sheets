using Domain.Characters;

namespace UI.Services;

public interface IStorageService
{
	Task<Character> Load(string id);
	Task<IReadOnlyCollection<Character>> LoadAll();
	Task<string> Save(Character character);
}