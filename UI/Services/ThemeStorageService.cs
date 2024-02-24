using Blazored.LocalStorage;
using Models.Settings;
using Persistence.Json;

namespace UI.Services;

public class ThemeStorageService(ILocalStorageService storageService, ISerializer serializer) : IThemeStorageService
{
	readonly ILocalStorageService storageService = storageService;
	readonly ISerializer serializer = serializer;
	const string GlobalThemeKey = "global-theme";

	public async Task SaveGlobalTheme(ThemeSetting themeSetting)
	{
		var json = this.serializer.Serialize(themeSetting);
		await this.storageService.SetItemAsStringAsync(GlobalThemeKey, json);
	}

	public async Task<ThemeSetting?> LoadGlobalTheme()
	{
		var json = await this.storageService.GetItemAsStringAsync(GlobalThemeKey);

		if (json == null)
			return null;

		return this.serializer.DeserializeTheme(json);
	}
}
