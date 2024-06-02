using Microsoft.AspNetCore.Components;
using Models.Characters;
using Models.Settings;
using UI.Services;

namespace UI.Components.CharacterSheet;
public partial class SheetAbility
{
	[Parameter, EditorRequired]
	public bool IsFixMode { get; set; } = false;

	[Parameter, EditorRequired]
	public Character? Character { get; set; }

	[Parameter, EditorRequired]
	public PlaybookSpecialAbility? Ability { get; set; }

	[Parameter, EditorRequired]
	public GameSetting? GameSetting { get; set; }

	Playbook? Playbook => Character?.Playbook;

	SpecialAbilitySetting? Setting =>
		Ability != null ? GameSetting?.FindAbility(Playbook?.Name, Ability)
		?? new(Ability.Name, 1, Ability.Description)
		: null;

	void OverwriteDescription(string description)
	{
		var hadSpecialArmor = Character!.HasSpecialArmor;
		var changed = Ability!.OverwriteDescription(description);

		if (!changed)
			return;

		if (hadSpecialArmor != Character!.HasSpecialArmor)
			SheetJank.NotifyAbilitiesChanged();
	}

	void RemoveAbility(PlaybookSpecialAbility ability)
	{
		Playbook!.RemoveAbility(ability);
		SheetJank.NotifyAbilitiesChanged();
	}

	string EditAbilityLabel =>
		$"Edit {Ability!.Name} Description";

	string RemoveAbilityLabel =>
		$"Unlearn {Ability!.Name}";

	string TextFieldId =>
		$"text-field-{Ability!.Name}";
}