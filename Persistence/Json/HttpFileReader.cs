namespace Persistence.Json;

public class HttpFileReader : IFileReader
{
	private readonly HttpClient http;
	private const string DataFolder = "data";

	public HttpFileReader(HttpClient http)
	{
		this.http = http;
	}

	public async Task<string> ReadFile(string fileName) =>
		await this.http.GetStringAsync($"{DataFolder}/{fileName}");
}
