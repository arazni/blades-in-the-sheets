using Models.Settings;
using Persistence.Json.DataModels;

namespace Persistence.Json;

public interface ILoader
{
	Task<GameFile[]> LoadGameFiles();
	Task<GameSetting> LoadSetting(string gameStem);
	Task<GameSetting> LoadSetting(string gameName, string language);
	Task<GameSetting> LoadSetting(GameFile gameFile);
}