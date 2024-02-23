using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;
using UI.Services;

namespace UI.Pages;
public partial class ThemeSettings
{
	[Inject]
	public IThemeSettingService ThemeSettingService { get; set; } = default!;

	[Inject]
	public IJSRuntime JS { get; set; } = default!;

	IJSObjectReference? module = default;

	ThemeButtonData[] ButtonDatas { get; set; } = [];

	protected override void OnInitialized()
	{
		ButtonDatas =
		[
			new(ThemeSettingService.DefaultTheme, "Default Theme"),
			new(ThemeSettingService.LightTheme, "Light Theme"),
			new(ThemeSettingService.BladesTheme, "Blades in the Dark Theme"),
			new(ThemeSettingService.ScumTheme, "Scum and Villainy Theme")
		];

		base.OnInitialized();
	}

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			foreach (var buttonData in ButtonDatas)
				await ThemeSettingService.SetElementTheme(buttonData.FluentButton!.Element, buttonData.ThemeSetting);

			module = await JS.InvokeAsync<IJSObjectReference>("import", "./Pages/ThemeSettings.razor.js");
		}

		await base.OnAfterRenderAsync(firstRender);
	}

	async Task SetGlobalTheme(ThemeSetting themeSetting)
	{
		ThemeSettingService.SetGlobalTheme(themeSetting);
		await Task.Delay(1); // hacky trick learned from microsoft's blazor fluent ui team
		await module!.InvokeVoidAsync("fixBodyBackgroundColor", themeSetting.ScareColor);
	}

	class ThemeButtonData(ThemeSetting themeSetting, string text)
	{
		public FluentButton? FluentButton { get; set; }

		public ThemeSetting ThemeSetting { get; set; } = themeSetting;

		public string Text { get; set; } = text;

		public string Class => ThemeSetting.BaseLayerLuminence < .5f ? "black-text" : string.Empty;
	}
}