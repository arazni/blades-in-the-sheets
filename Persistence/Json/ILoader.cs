using Models.Settings;

namespace Persistence.Json;

public interface ILoader
{
	Task<GameSetting> LoadSettings(string gameName);
}