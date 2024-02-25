using Microsoft.AspNetCore.Components;
using Models.Characters;
using Models.Settings;

namespace UI.Components.CharacterSheet;
public partial class SheetDossierCard
{
	[Parameter, EditorRequired]
	public Dossier Dossier { get; set; } = new();

	[Parameter, EditorRequired]
	public GameSetting GameSetting { get; set; } = EmptyGameSetting.Game();

	[Parameter]
	public string AliasSynonym { get; set; } = Models.Settings.Constants.DefaultNames.Alias;

	bool IsFixMode { get; set; } = false;

	string BackgroundLabel => $"Background: {Dossier.Background.Name}";

	string HeritageLabel => $"Heritage: {Dossier.Heritage.Name}";

	string ViceLabel => $"Vice: {Dossier.Vice.Name}";
}