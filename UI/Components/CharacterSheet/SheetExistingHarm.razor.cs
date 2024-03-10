using Microsoft.AspNetCore.Components;
using Models.Characters;

namespace UI.Components.CharacterSheet;
public partial class SheetExistingHarm
{
	[Parameter, EditorRequired]
	public bool IsFixMode { get; set; }

	[Parameter, EditorRequired]
	public MonitorHarm Harm { get; set; } = MonitorHarm.Empty();

	[Parameter, EditorRequired]
	public HarmIntensity Intensity { get; set; }

	string PrefixText => Intensity switch
	{
		HarmIntensity.Lesser => "Reduce effect",
		HarmIntensity.Moderate => "-1 to dice pool",
		HarmIntensity.Severe => "Incapacitated",
		HarmIntensity.Fatal => "Catastrophic or fatal",
		_ => throw new NotImplementedException()
	};

	IReadOnlyCollection<string> HarmDescriptions =>
		Harm.HarmsAtIntensity(Intensity);

	void RemoveHarm(string description)
	{
		Harm.RemoveHarm(description, Intensity);
		SheetJank.NotifyHarmChanged();
	}

	static string AriaLabel(string description) =>
		"Remove " + description;
}