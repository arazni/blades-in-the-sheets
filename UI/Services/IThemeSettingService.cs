using Microsoft.AspNetCore.Components;
using Models.Settings;

namespace UI.Services;
public interface IThemeSettingService
{
	ThemeSetting DefaultTheme { get; }
	ThemeSetting LightTheme { get; }
	ThemeSetting BladesTheme { get; }
	ThemeSetting ScumTheme { get; }

	event Action? ThemeChanged;

	string GetGlobalAccentBaseColor();
	float GetGlobalBaseLayerLuminence();
	string GetGlobalNeutralBaseColor();
	string GetGlobalThemeScareColor();
	Task LoadGlobalTheme();
	Task SetElementTheme(ElementReference element, ThemeSetting theme);
	Task SetGlobalTheme(ThemeSetting theme);
}
