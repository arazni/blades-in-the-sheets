namespace Persistence.Json;

public interface IFileReader
{
	Task<string> ReadFile(string fileName);
}