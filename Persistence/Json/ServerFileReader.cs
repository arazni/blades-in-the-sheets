using Models.Common;
using Newtonsoft.Json;
using Persistence.Json.DataModels;

namespace Persistence.Json;

public class ServerFileReader : IFileReader
{
	private readonly string directoryPath = GetDataDirectoryPath();

	private static string GetDataDirectoryPath()
	{
		var cwd = Directory.GetCurrentDirectory().Split(Path.DirectorySeparatorChar);
		var binIndex = cwd.TakeWhile(directory => directory != "bin").Count();
		cwd[binIndex - 1] = "UI";

		DirectoryInfo directory = new(cwd.Join(Path.DirectorySeparatorChar.ToString()));

		directory = directory.EnumerateDirectories()
			.FirstOrDefault(d => d.Name == "wwwroot")
			?.EnumerateDirectories()
			.FirstOrDefault(d => d.Name == "data")
			?? throw new DirectoryNotFoundException($"data is not a subdirectory of {directory.FullName}");

		return directory.FullName;
	}

	public async Task<GameFile[]> AllFiles()
	{
		var path = Path.Combine(this.directoryPath, "games.json");

		var text = await File.ReadAllTextAsync(path);

		return JsonConvert.DeserializeObject<GameFile[]>(text)
			?? throw new FileNotFoundException($"games.json is missing from {this.directoryPath}");
	}

	public async Task<string> ReadFile(string fileName)
	{
		var path = Path.Combine(this.directoryPath, fileName);

		var text = await File.ReadAllTextAsync(path);

		return text;
	}
}
