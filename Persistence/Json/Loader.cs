using Models.Common;
using Models.Settings;
using Newtonsoft.Json;

namespace Persistence.Json;

public class Loader : ILoader
{
	private readonly IFileReader fileReader;

	public Loader(IFileReader fileReader)
	{
		this.fileReader = fileReader;
	}

	public async Task<GameSetting> LoadSettings(string gameName)
	{
		var gameFiles = await this.fileReader.AllFiles();

		var gameFile = gameFiles.FirstOrDefault(f => gameName.In(f.Name, f.File))
			?? throw new ArgumentOutOfRangeException(nameof(gameName), $"Cannot recognize {gameName} in available settings files: {gameFiles.Join(", ")}");

		var gameJson = await this.fileReader.ReadFile(gameFile.File)
			?? throw new FileNotFoundException($"Cannot find file {gameFile}. Contact the software developer.");

		var loadedGameSetting = JsonConvert.DeserializeObject<GameSetting>(gameJson)
			?? throw new JsonSerializationException($"The file {gameFile} cannot deserialize into a {nameof(GameSetting)} correctly. You may need to update the file. See the game-settings-schema.json file in the repo.");

		return loadedGameSetting;
	}
}
