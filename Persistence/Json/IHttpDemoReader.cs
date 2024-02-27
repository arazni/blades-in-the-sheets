using Models.Characters;

namespace Persistence.Json;
public interface IHttpDemoReader
{
	Task<Character> GetDemoCharacter();
}
