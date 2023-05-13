using Models.Settings;
using Persistence.Json.DataModels;

namespace Persistence.Json;

public interface ILoader
{
	Task<GameFile[]> LoadGameFiles();
	Task<GameSetting> LoadSetting(string gameName);
	Task<GameSetting> LoadSetting(GameFile gameFile);
}