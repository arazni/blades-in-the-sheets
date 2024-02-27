using Models.Characters;

namespace Persistence.Json;
public class HttpDemoReader(HttpClient http, ISerializer serializer) : IHttpDemoReader
{
	private readonly HttpClient http = http;
	private readonly ISerializer serializer = serializer;
	private const string DataFolder = "data";

	public async Task<Character> GetDemoCharacter()
	{
		var json = await this.http.GetStringAsync($"{DataFolder}/demo-character.json");
		return this.serializer.Deserialize(json);
	}
}
