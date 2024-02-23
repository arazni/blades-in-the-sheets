using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;

namespace UI.Shared;
public sealed partial class MainLayout
{
	IJSObjectReference? module = default;

	public bool IsNavMenuOpen { get; set; } = false;

	public string NavMenuClass => IsNavMenuOpen ? ""
		: "dn";

	public string OpenNavButtonClass => IsNavMenuOpen ? "dn" : string.Empty;

	public string CloseNavButtonClass => IsNavMenuOpen ? string.Empty : "dn";

	public FluentDesignSystemProvider ThemeProvider { get; set; } = default!;

	public void OnThemeChanged()
	{
		StateHasChanged();
	}

	protected override void OnInitialized()
	{
		ThemeSettingService.ThemeChanged += OnThemeChanged;

		base.OnInitialized();
	}

	public void Dispose()
	{
		ThemeSettingService.ThemeChanged -= OnThemeChanged;
	}

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			module = await JS.InvokeAsync<IJSObjectReference>("import", "./Pages/ThemeSettings.razor.js");
			await module.InvokeVoidAsync("fixBodyBackgroundColor", ThemeSettingService.DefaultTheme.ScareColor);
		}

		await base.OnAfterRenderAsync(firstRender);
	}

	//	private static MudTheme InitializeMudTheme()
	//{
	//	var theme = new MudTheme();
	//	theme.PaletteDark.Primary = Colors.DeepOrange.Lighten2;
	//	theme.PaletteDark.Secondary = Colors.Red.Accent4;
	//	theme.PaletteDark.Tertiary = Colors.DeepPurple.Accent2;
	//	return theme;
	//}
}