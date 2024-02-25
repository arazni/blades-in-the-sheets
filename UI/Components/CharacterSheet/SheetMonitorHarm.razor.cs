using Microsoft.AspNetCore.Components;
using Models.Characters;
using Models.Common;
using UI.Services;

namespace UI.Components.CharacterSheet;
public sealed partial class SheetMonitorHarm
{
	[Parameter, EditorRequired]
	public bool IsFixMode { get; set; }

	[Parameter, EditorRequired]
	public MonitorHarm Harm { get; set; } = MonitorHarm.Empty();

	string HarmDescription { get; set; } = string.Empty;

	HarmIntensity Intensity { get; set; } = HarmIntensity.Lesser;

	protected override void OnInitialized()
	{
		SheetJank.HarmChanged += StateHasChanged;

		base.OnInitialized();
	}

	protected override void OnParametersSet()
	{
		Intensity = Harm.AvailableIntensities.FirstOrDefault();

		base.OnParametersSet();
	}

	string HarmPlaceholder => Intensity switch
	{
		HarmIntensity.Fatal => "Electrocuted, Drowned, or Stabbed in the Heart.",
		HarmIntensity.Severe => "Impaled, Broken Leg, Shot in Chest, Badly Burned, or Terrified.",
		HarmIntensity.Moderate => "Exhausted, Deep Cut to Arm, Concussion, Panicked, or Seduced.",
		HarmIntensity.Lesser => "Battered, Drained, Distracted, Scared, Confused.",
		_ => "Choose a harm severity above"
	};

	bool CanAddHarm =>
		HarmDescription.HasInk();

	void AddHarm()
	{
		var intensity = Intensity;

		Harm.AddHarm(HarmDescription, intensity);

		ResetAddHarm();
	}

	void ResetAddHarm()
	{
		HarmDescription = string.Empty;
		Intensity = Harm.AvailableIntensities.FirstOrDefault();
	}

	public void Dispose()
	{
		SheetJank.HarmChanged -= StateHasChanged;
	}
}