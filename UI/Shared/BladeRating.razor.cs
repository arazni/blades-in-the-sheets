using Microsoft.AspNetCore.Components;
using Models.Settings;

namespace UI.Shared;

public partial class BladeRating
{
	[Parameter, EditorRequired] public int MaxValue { get; set; }

	[Parameter, EditorRequired] public string CheckboxAriaLabelSuffix { get; set; } = string.Empty;

	[Parameter, EditorRequired] public int Value { get; set; }

	[Parameter] public EventCallback<int> ValueChanged { get; set; }

	[Parameter] public bool ReadOnly { get; set; } = false;

	[Parameter] public string Id { get; set; } = string.Empty;

	protected int previousMaxValue = -1;

	protected AccessibilitySetting.RatingImplementation RatingImplementation { get; set; }

	protected override async Task OnInitializedAsync()
	{
		RatingImplementation = await AccessibilityService.GetRatingImplementation();
		AccessibilityService.SettingChanged += OnImplementationChanged;
		await base.OnInitializedAsync();
	}

	void OnImplementationChanged(AccessibilitySetting setting)
	{
		RatingImplementation = setting.Rating;
		StateHasChanged();
	}

	public async Task SliderChanged(int value)
	{
		Value = value;

		if (ValueChanged.HasDelegate)
			await ValueChanged.InvokeAsync(Value);
	}

	int SliderMinimumPixelWidth =>
		MaxValue * 20
		+ (MaxValue - 1) * 5
		+ (MaxValue / 3) * 10;

	public void Dispose()
	{
		AccessibilityService.SettingChanged -= OnImplementationChanged;
		GC.SuppressFinalize(this);
	}
}