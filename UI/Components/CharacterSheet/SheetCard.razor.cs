using Microsoft.AspNetCore.Components;

namespace UI.Components.CharacterSheet;
public partial class SheetCard
{
	[Parameter]
	public bool HasFixMode { get; set; } = true;

	[Parameter]
	public bool IsFixMode { get; set; } = false;

	[Parameter, EditorRequired]
	public string Header { get; set; } = string.Empty;

	[Parameter, EditorRequired]
	public RenderFragment? ChildContent { get; set; }

	[Parameter]
	public EventCallback<bool> IsFixModeChanged { get; set; }

	bool IsHideMode { get; set; } = false;

	string HideModeStyle => IsHideMode ? DisplayNone : string.Empty;

	string EnableHideModeStyle => IsHideMode ? DisplayNone : string.Empty;

	string DisableHideModeStyle => !IsHideMode ? DisplayNone : string.Empty;

	string EnableFixModeStyle => !ShowEnableFixMode ? DisplayNone : string.Empty;

	string DisableFixModeStyle => !ShowDisableFixMode ? DisplayNone : string.Empty;

	bool ShowEnableFixMode => !IsHideMode && HasFixMode && !IsFixMode;

	bool ShowDisableFixMode => !IsHideMode && HasFixMode && IsFixMode;

	async Task ClickFixMode(bool isEnable)
	{
		IsFixMode = isEnable;
		await IsFixModeChanged.InvokeAsync(IsFixMode);
	}

	async Task EnableFixMode() => await ClickFixMode(true);

	async Task DisableFixMode() => await ClickFixMode(false);

	const string DisplayNone = "display: none;";
}