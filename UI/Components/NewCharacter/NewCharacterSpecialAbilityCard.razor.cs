using Microsoft.AspNetCore.Components;
using Models.Characters;
using Models.Common;
using Models.Settings;

namespace UI.Components.NewCharacter;
public partial class NewCharacterSpecialAbilityCard
{
	[Parameter, EditorRequired]
	public Character Character { get; set; } = Character.Empty();

	[Parameter, EditorRequired]
	public GameSetting GameSetting { get; set; } = EmptyGameSetting.Game();

	Playbook Playbook => Character.Playbook;

	PlaybookSpecialAbility[] AvailableAbilities { get; set; } = [];

	PlaybookSpecialAbility? ChosenAbility { get; set; }

	PlaybookSpecialAbility[] AvailableStartingAbilities { get; set; } = [];

	PlaybookSpecialAbility? ChosenStartingAbility { get; set; }

	bool HasStartingAbilities => AvailableStartingAbilities.Any();

	private void OnChosenAbilityChanged(PlaybookSpecialAbility value)
	{
		Playbook.ClearAbilities();

		if (ChosenStartingAbility != null)
			Playbook.TakeAbility(ChosenStartingAbility);

		ChosenAbility = value;
		Playbook.TakeAbility(ChosenAbility);
	}

	private void OnChosenStartingAbilityChanged(PlaybookSpecialAbility value)
	{
		Playbook.ClearAbilities();

		ChosenStartingAbility = value;
		Playbook.TakeAbility(ChosenStartingAbility);

		if (ChosenAbility != null)
			Playbook.TakeAbility(ChosenAbility);
	}

	void ResetChosenStartingAbilities()
	{
		AvailableStartingAbilities = GameSetting.GetAvailableStartingAbilities(Character.Dossier.Heritage.Name, Character.Playbook.Name);

		if (!HasStartingAbilities)
			return;

		if (!ChosenStartingAbility.In(AvailableStartingAbilities))
			OnChosenStartingAbilityChanged(AvailableStartingAbilities.First());

		StateHasChanged();
	}

	protected override void OnParametersSet()
	{
		AvailableAbilities = GameSetting.GetPlaybookSetting(Playbook.Name)
			.GetAvailableAbilities();

		if (ChosenStartingAbility == null && HasStartingAbilities)
			OnChosenStartingAbilityChanged(AvailableStartingAbilities.First());

		if (ChosenAbility == null && AvailableAbilities.Any())
			OnChosenAbilityChanged(AvailableAbilities.First());

		base.OnParametersSet();
	}

	private static string DisplayText(PlaybookSpecialAbility ability) =>
		ability.TimesTakeable == 1 ? ability.Name
		: $"{ability.Name} (take once of {ability.TimesTakeable} possible times)";

	protected override void OnInitialized()
	{
		CreationJank.HeritageChanged += ResetChosenStartingAbilities;

		base.OnInitialized();
	}

	public void Dispose()
	{
		CreationJank.HeritageChanged -= ResetChosenStartingAbilities;
		GC.SuppressFinalize(this);
	}
}