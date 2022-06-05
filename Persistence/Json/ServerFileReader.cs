namespace Persistence.Json;

public class ServerFileReader : IFileReader
{
	private readonly string directoryPath = GetDataDirectoryPath();

	private static string GetDataDirectoryPath()
	{
		DirectoryInfo directory = new(Directory.GetCurrentDirectory());

		directory = directory.EnumerateDirectories()
			.FirstOrDefault(d => d.Name == "Data")
			?? throw new DirectoryNotFoundException($"Data is not a subdirectory of {directory.FullName}");

		return directory.FullName;
	}

	public async Task<string> ReadFile(string fileName)
	{
		var path = Path.Combine(this.directoryPath, fileName);

		var text = await File.ReadAllTextAsync(path);

		return text;
	}
}
