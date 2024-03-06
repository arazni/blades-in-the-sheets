using Models.Common;
using Models.Settings;
using Newtonsoft.Json;
using Persistence.Json.DataModels;

namespace Persistence.Json;

public class Loader : ILoader
{
	private readonly IFileReader fileReader;

	public Loader(IFileReader fileReader)
	{
		this.fileReader = fileReader;
	}

	public async Task<GameFile[]> LoadGameFiles() =>
		await this.fileReader.AllFiles();

	public async Task<GameSetting> LoadSetting(string gameStem)
	{
		var gameFiles = await LoadGameFiles();

		var gameFile = gameFiles.FirstOrDefault(f => gameStem.In(f.Stem))
			?? throw new ArgumentOutOfRangeException(nameof(gameStem), $"Cannot recognize {gameStem} in available settings files: {gameFiles.Join(", ")}");

		return await LoadSetting(gameFile);
	}

	public async Task<GameSetting> LoadSetting(string gameName, string language)
	{
		var gameFiles = await LoadGameFiles();

		var gameFile = gameFiles.FirstOrDefault(f => gameName.In(f.Name, f.Stem) && language.Like(f.Language))
			?? throw new ArgumentOutOfRangeException(nameof(gameName), $"Cannot recognize {language} {gameName} in available settings files: {gameFiles.Join(", ")}");

		return await LoadSetting(gameFile);
	}

	public async Task<GameSetting> LoadSetting(GameFile gameFile)
	{
		string gameJson;

		try
		{
			gameJson = await this.fileReader.ReadFile(gameFile.Stem);
		}
		catch (Exception ex)
		{
			throw new FileNotFoundException($"Cannot load file {gameFile}. Contact the software developer.", ex);
		}

		GameSetting loadedGameSetting;
		try
		{
			loadedGameSetting = JsonConvert.DeserializeObject<GameSetting>(gameJson)
				?? throw new Exception();
		}
		catch (Exception ex)
		{
			throw new JsonSerializationException($"The file {gameFile} cannot deserialize into a {nameof(GameSetting)} correctly. You may need to update the file. See the game-settings-schema.json file in the repo.", ex);
		}

		return loadedGameSetting;
	}
}
