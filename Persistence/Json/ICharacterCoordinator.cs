using Models.Characters;
using Models.Settings;

namespace Persistence.Json;

public interface ICharacterCoordinator
{
	Task<Character> InitializeCharacter(string gameName, string language, string playbookOption);
	Character InitializeCharacter(GameSetting gameSetting, string playbookOption);
}
