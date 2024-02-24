using Models.Settings;

namespace UI.Services;
public interface IThemeStorageService
{
	Task<ThemeSetting?> LoadGlobalTheme();
	Task SaveGlobalTheme(ThemeSetting themeSetting);
}