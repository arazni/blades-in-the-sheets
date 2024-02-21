using Microsoft.FluentUI.AspNetCore.Components;

namespace UI.Shared;
public sealed partial class MainLayout
{
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

	//	private static MudTheme InitializeMudTheme()
	//{
	//	var theme = new MudTheme();
	//	theme.PaletteDark.Primary = Colors.DeepOrange.Lighten2;
	//	theme.PaletteDark.Secondary = Colors.Red.Accent4;
	//	theme.PaletteDark.Tertiary = Colors.DeepPurple.Accent2;
	//	return theme;
	//}
}