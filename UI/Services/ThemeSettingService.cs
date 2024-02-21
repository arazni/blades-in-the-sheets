using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components.DesignTokens;

namespace UI.Services;

public class ThemeSettingService(NeutralBaseColor neutralBaseColor, AccentBaseColor accentBaseColor, BaseLayerLuminance baseLayerLuminance) : IThemeSettingService
{
	private readonly NeutralBaseColor neutralBaseColor = neutralBaseColor;
	private readonly AccentBaseColor accentBaseColor = accentBaseColor;
	private readonly BaseLayerLuminance baseLayerLuminance = baseLayerLuminance;

	private ThemeSetting GlobalTheme { get; set; } = Default;

	public event Action? ThemeChanged;

	void NotifyGlobalThemeChanged() => ThemeChanged?.Invoke();

	public string GetGlobalNeutralBaseColor() => GlobalTheme.NeutralBaseColor;

	public string GetGlobalAccentBaseColor() => GlobalTheme.AccentBaseColor;

	public float GetGlobalBaseLayerLuminence() => GlobalTheme.BaseLayerLuminence;

	public void SetGlobalTheme(ThemeSetting theme)
	{
		GlobalTheme = theme;
		NotifyGlobalThemeChanged();
	}

	public async Task SetElementTheme(ElementReference element, ThemeSetting theme)
	{
		await this.neutralBaseColor.SetValueFor(element, theme.NeutralBaseColor.ToSwatch());
		await this.accentBaseColor.SetValueFor(element, theme.AccentBaseColor.ToSwatch());
		await this.baseLayerLuminance.SetValueFor(element, theme.BaseLayerLuminence);
	}

	private static ThemeSetting Default => new()
	{
		AccentBaseColor = "#0078d4",
		NeutralBaseColor = "#808080",
		BaseLayerLuminence = 0.15f
	};

	public ThemeSetting DefaultTheme => Default;

	public ThemeSetting LightTheme => new()
	{
		AccentBaseColor = "#0078d4",
		NeutralBaseColor = "#808080",
		BaseLayerLuminence = 0.98f
	};

	public ThemeSetting BladesTheme => new()
	{
		AccentBaseColor = "#e75b06",
		NeutralBaseColor = "#ffffff",
		BaseLayerLuminence = 0.15f
	};

	public ThemeSetting ScumTheme => new()
	{
		AccentBaseColor = "#DA70D6", //631ca6
		NeutralBaseColor = "#4682B4",
		BaseLayerLuminence = 1f
	};
}