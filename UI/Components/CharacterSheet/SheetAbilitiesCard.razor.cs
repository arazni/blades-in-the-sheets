using Microsoft.AspNetCore.Components;
using Models.Characters;
using Models.Settings;
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

	IReadOnlyCollection<PlaybookSpecialAbility> AvailableAbilities = Array.Empty<PlaybookSpecialAbility>();

	IReadOnlyCollection<PlaybookSpecialAbility> LearnableAbilities =>
		AvailableAbilities.Where
		(
			ability =>
			{
				if (!Playbook.AbilitiesByName.TryGetValue(ability.Name, out var knownAbility))
					return true;

				return !knownAbility.IsCompletelyLearned;
			}
		).ToArray();

	PlaybookSpecialAbility? SelectedAbility { get; set; }

	protected override void OnParametersSet()
	{
		AvailableAbilities = GameSetting.GetPlaybookSetting(Playbook.Name)
			.GetAvailableAbilities();
	}

	protected override void OnInitialized()
	{
		SheetJank.AbilitiesChanged += StateHasChanged;

		base.OnInitialized();
	}

	public void Dispose()
	{
		SheetJank.AbilitiesChanged -= StateHasChanged;
	}

	bool CanAddAbility =>
		IsFixMode || Playbook.Experience.CanLevelUp;

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

		SelectedAbility = null;
		SheetJank.NotifyAbilitiesChanged();
	}
}