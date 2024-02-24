﻿using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components.DesignTokens;
using Models.Settings;

namespace UI.Services;

public class ThemeSettingService(NeutralBaseColor neutralBaseColor, AccentBaseColor accentBaseColor, BaseLayerLuminance baseLayerLuminance, IThemeStorageService themeStorageService) : IThemeSettingService
{
	readonly NeutralBaseColor neutralBaseColor = neutralBaseColor;
	readonly AccentBaseColor accentBaseColor = accentBaseColor;
	readonly BaseLayerLuminance baseLayerLuminance = baseLayerLuminance;
	readonly IThemeStorageService themeStorageService = themeStorageService;

	private ThemeSetting GlobalTheme { get; set; } = Default;

	public event Action? ThemeChanged;

	void NotifyGlobalThemeChanged() => ThemeChanged?.Invoke();

	public string GetGlobalNeutralBaseColor() => GlobalTheme.NeutralBaseColor;

	public string GetGlobalAccentBaseColor() => GlobalTheme.AccentBaseColor;

	public float GetGlobalBaseLayerLuminence() => GlobalTheme.BaseLayerLuminence;

	public string GetGlobalThemeScareColor() => GlobalTheme.ScareColor;

	public async Task SetGlobalTheme(ThemeSetting theme)
	{
		GlobalTheme = theme;
		await this.themeStorageService.SaveGlobalTheme(GlobalTheme);
		NotifyGlobalThemeChanged();
	}

	public async Task LoadGlobalTheme()
	{
		GlobalTheme = await this.themeStorageService.LoadGlobalTheme()
			?? DefaultTheme;
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
		BaseLayerLuminence = 0.15f,
		ScareColor = "#E9835E"
	};

	public ThemeSetting DefaultTheme => Default;

	public ThemeSetting LightTheme => new()
	{
		AccentBaseColor = "#0078d4",
		NeutralBaseColor = "#808080",
		BaseLayerLuminence = 0.98f,
		ScareColor = "#BC2F32"
	};

	public ThemeSetting BladesTheme => new()
	{
		AccentBaseColor = "#e75b06",
		NeutralBaseColor = "#808080",
		BaseLayerLuminence = 0.15f,
		ScareColor = "#FF6432"
	};

	public ThemeSetting ScumTheme => new()
	{
		AccentBaseColor = "#631ca6", //"#DA70D6"
		NeutralBaseColor = "#4682B4",
		BaseLayerLuminence = 1f,
		ScareColor = "#BC2F32"
	};
}