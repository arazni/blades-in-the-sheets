using Microsoft.AspNetCore.Components;

namespace UI.Shared;

public partial class BladeRating
{
	[Parameter] public int MinValue { get; set; } = 0;

	[Parameter, EditorRequired] public int MaxValue { get; set; }

	[Parameter, EditorRequired] public string CheckboxAriaLabelSuffix { get; set; } = string.Empty;

	[Parameter, EditorRequired] public int Value { get; set; }

	[Parameter] public EventCallback<int> ValueChanged { get; set; }

	protected bool[] ButtonsAreChecked { get; set; } = [];

	protected BladeRatingCheckbox?[] Buttons { get; set; } = [];

	protected int previousMaxValue = -1;

	protected override void OnParametersSet()
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

		base.OnParametersSet();
	}

	public string CheckboxLabel(int index) => $"{index + 1} of {MaxValue} {CheckboxAriaLabelSuffix}";

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
}