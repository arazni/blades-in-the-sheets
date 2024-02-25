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

	protected bool[] ButtonsAreChecked { get; set; } = [];

	protected BladeRatingCheckbox?[] Buttons { get; set; } = [];

	protected int previousMaxValue = -1;

	protected AccessibilitySetting.RatingImplementation RatingImplementation { get; set; }

	protected override async Task OnInitializedAsync()
	{
		InitializeButtons();
		RatingImplementation = await AccessibilityService.GetRatingImplementation();
		AccessibilityService.SettingChanged += OnImplementationChanged;
		await base.OnInitializedAsync();
	}

	protected override void OnParametersSet()
	{
		InitializeButtons();

		base.OnParametersSet();
	}

	private void InitializeButtons()
	{
		if (this.previousMaxValue != MaxValue)
		{
			Buttons = Enumerable.Range(0, MaxValue)
			.Select(i => (BladeRatingCheckbox?)null)
			.ToArray();

			previousMaxValue = MaxValue;
		}

		ButtonsAreChecked = Enumerable.Range(0, MaxValue)
			.Select(i => i < Value)
			.ToArray();
	}

	void OnImplementationChanged(AccessibilitySetting setting)
	{
		RatingImplementation = setting.Rating;
		StateHasChanged();
	}

	public string CheckboxLabel(int index) => $"{index + 1} of {MaxValue} currently {Value} {CheckboxAriaLabelSuffix}";

	public string CheckboxClass(int index) => " blade-rating-box " + (MaxValue > 5 && (index + 1) % 3 == 0 ? " spacing " : string.Empty);

	public async Task CheckboxChanged(int index)
	{
		var isChecked = !ButtonsAreChecked[index];

		if (!isChecked && Value == index + 1)
			Value = 0;
		else
			Value = index + 1;

		for (int i = 0; i < ButtonsAreChecked.Length; i++)
			ButtonsAreChecked[i] = i < Value;

		if (ValueChanged.HasDelegate)
			await ValueChanged.InvokeAsync(Value);

		Buttons[index]!.HackStateChanged();
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