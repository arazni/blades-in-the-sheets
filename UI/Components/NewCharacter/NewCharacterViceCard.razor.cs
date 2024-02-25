using Microsoft.AspNetCore.Components;
using Models.Characters;
using Models.Settings;

namespace UI.Components.NewCharacter;
public partial class NewCharacterViceCard
{
	[Parameter, EditorRequired]
	public DossierVice Vice { get; set; } = new();

	[Parameter, EditorRequired]
	public ViceSetting[] ViceSettings { get; set; } = EmptyGameSetting.Vices();

	bool HasExamples => Examples().Any();

	string Elaboration() =>
		ViceSettings.FirstOrDefault(v => v.Name == Vice.Name)?.Description
			?? string.Empty;

	string[] Examples() =>
		ViceSettings.FirstOrDefault(v => v.Name == Vice.Name)?.Sources
			?? [];

	string ExamplePlaceholder() =>
		Examples()
			.FirstOrDefault()
			?? string.Empty;

	string HeaderText() =>
		HasExamples ? "Choose a vice and its purveyor"
		: "Choose a vice";

	string HelperText() =>
		HasExamples ? "A detail about your purveyor and how you indulge in your vice."
		: "A detail about how you indulge in your vice.";
}