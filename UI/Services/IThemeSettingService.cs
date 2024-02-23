using Microsoft.AspNetCore.Components;

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
	Task SetElementTheme(ElementReference element, ThemeSetting theme);
	void SetGlobalTheme(ThemeSetting theme);
}

public class ThemeSetting
{
	public required string NeutralBaseColor { get; set; }

	public required string AccentBaseColor { get; set; }

	public required float BaseLayerLuminence { get; set; }

	public required string ScareColor { get; set; }
}