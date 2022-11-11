using Models.Characters;

namespace Persistence.Json;

public interface ISaver
{
	Task Save(Character character);
}