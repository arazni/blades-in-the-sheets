using Microsoft.AspNetCore.Components;
using Models.Characters;
using Models.Settings;

namespace UI.Components.CharacterSheet;
public partial class SheetNotebookCard
{
	[EditorRequired, Parameter]
	public Character Character { get; set; } = Character.Empty();

	[EditorRequired, Parameter]
	public GameSetting GameSetting { get; set; } = EmptyGameSetting.Game();

	string PlaybookExperienceCondition => GameSetting.GetPlaybookSetting(Character.Playbook.Name)
		.ExperienceCondition;
}