using Newtonsoft.Json;
using Persistence.Json.DataModels;

namespace Persistence.Json;

public class HttpFileReader : IFileReader
{
	private readonly HttpClient http;
	private const string DataFolder = "data";

	public HttpFileReader(HttpClient http)
	{
		this.http = http;
	}

	public async Task<string> ReadFile(string fileStem) =>
		await this.http.GetStringAsync($"{DataFolder}/{fileStem}.json");

	public async Task<GameFile[]> AllFiles()
	{
		var response = await this.http.GetAsync($"{DataFolder}/games.json");

		var raw = await response.Content.ReadAsStringAsync();

		return JsonConvert.DeserializeObject<GameFile[]>(raw)
			?? Array.Empty<GameFile>();
	}
}
