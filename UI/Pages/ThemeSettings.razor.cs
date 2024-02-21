using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using UI.Services;

namespace UI.Pages;
public partial class ThemeSettings
{
	[Inject]
	public IThemeSettingService ThemeSettingService { get; set; } = default!;

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
		}

		await base.OnAfterRenderAsync(firstRender);
	}
}