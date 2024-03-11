using Microsoft.AspNetCore.Components;
using Models.Characters;
using Models.Settings;
using UI.Conveniences;
using UI.Services;

namespace UI.Components.CharacterSheet;
public sealed partial class SheetAbilitiesCard
{
	[Parameter, EditorRequired]
	public Character Character { get; set; } = Character.Empty();

	[Parameter, EditorRequired]
	public GameSetting GameSetting { get; set; } = EmptyGameSetting.Game();

	Playbook Playbook => Character.Playbook;

	public bool IsFixMode { get; set; } = false;

	IReadOnlyCollection<PlaybookSpecialAbility> KnownAbilities =>
		Playbook.Abilities;

	PlaybookSpecialAbility[] LearnableAbilities { get; set; } = [];

	PlaybookSpecialAbility? SelectedAbility { get; set; }

	protected override void OnParametersSet()
	{
		ReloadLearnableAbilities();

		base.OnParametersSet();
	}

	private void ReloadLearnableAbilities()
	{
		LearnableAbilities = GameSetting.GetDisplayableAbilities(Playbook, Playbook.Name);
		SelectedAbility = LearnableAbilities.FirstOrDefault();
	}

	protected override void OnInitialized()
	{
		SheetJank.AbilitiesChanged += HandleAbilitiesChanged;

		base.OnInitialized();
	}

	public void Dispose()
	{
		SheetJank.AbilitiesChanged -= HandleAbilitiesChanged;
	}

	bool CanAddAbility =>
		(IsFixMode || Playbook.Experience.CanLevelUp)
		&& LearnableAbilities.Any();

	bool CannotAddAbility =>
		!CanAddAbility;

	void AddAbility()
	{
		if (SelectedAbility == null)
			return;

		var success = Playbook.TakeAbility(SelectedAbility);

		if (!success)
			return;

		if (!IsFixMode)
			Playbook.Experience.Clear();

		ReloadLearnableAbilities();
		SheetJank.NotifyAbilitiesChanged();
	}

	void HandleAbilitiesChanged()
	{
		ReloadLearnableAbilities();
		StateHasChanged();
	}
}