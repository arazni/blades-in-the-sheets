using Microsoft.AspNetCore.Components;
using Models.Characters;
using Models.Settings;

namespace UI.Components.NewCharacter;
public partial class NewCharacterHeritageCard
{
	[Parameter, EditorRequired]
	public DossierHeritage Heritage { get; set; } = new();

	[Parameter, EditorRequired]
	public HeritageSetting[] HeritageSettings { get; set; } = EmptyGameSetting.Heritages();

	private string Elaboration() =>
		HeritageSettings.FirstOrDefault(h => h.Name == Heritage.Name)?.Description ?? string.Empty;

	protected override void OnParametersSet()
	{
		if (HeritageSettings.Any())
			Heritage.Name = HeritageSettings.First().Name;

		base.OnParametersSet();
	}
}