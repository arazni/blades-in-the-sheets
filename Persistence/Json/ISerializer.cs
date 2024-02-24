using Models.Characters;
using Models.Settings;

namespace Persistence.Json;

public interface ISerializer
{
	Character Deserialize(string json);
	ThemeSetting DeserializeTheme(string json);
	string Serialize(Character character);
	string Serialize(ThemeSetting themeSetting);
}