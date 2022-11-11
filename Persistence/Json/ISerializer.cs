using Models.Characters;

namespace Persistence.Json;

public interface ISerializer
{
	Character Deserialize(string json);
	string Serialize(Character character);
}