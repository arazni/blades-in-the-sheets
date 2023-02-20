using Models.Characters;

namespace Persistence.Json;

public interface ICharacterCoordinator
{
	Task<Character> InitializeCharacter(PlaybookOption option);
}