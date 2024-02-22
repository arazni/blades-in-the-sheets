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

	FluentButton? ButtonDefault { get; set; }

	FluentButton? ButtonLight { get; set; }

	FluentButton? ButtonBlades { get; set; }

	FluentButton? ButtonScum { get; set; }

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			await ThemeSettingService.SetElementTheme(ButtonDefault!.Element, ThemeSettingService.DefaultTheme);
			await ThemeSettingService.SetElementTheme(ButtonLight!.Element, ThemeSettingService.LightTheme);
			await ThemeSettingService.SetElementTheme(ButtonBlades!.Element, ThemeSettingService.BladesTheme);
			await ThemeSettingService.SetElementTheme(ButtonScum!.Element, ThemeSettingService.ScumTheme);
			module = await JS.InvokeAsync<IJSObjectReference>("import", "./Pages/ThemeSettings.razor.js");
		}

		await base.OnAfterRenderAsync(firstRender);
	}

	async Task SetGlobalTheme(ThemeSetting themeSetting)
	{
		ThemeSettingService.SetGlobalTheme(themeSetting);
		await Task.Delay(1); // hacky trick learned from microsoft's blazor fluent ui team
		await module!.InvokeVoidAsync("fixBodyBackgroundColor");
	}
}