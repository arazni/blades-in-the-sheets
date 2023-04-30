using Persistence.Json.DataModels;

namespace Persistence.Json;

public interface IFileReader
{
	Task<GameFile[]> AllFiles();
	Task<string> ReadFile(string fileName);
}