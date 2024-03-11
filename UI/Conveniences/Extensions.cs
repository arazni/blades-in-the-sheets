using Models.Characters;
using Models.Settings;

namespace UI.Conveniences;

public static class Extensions
{
	public static string LearnedTimesDisplayName(this PlaybookSpecialAbility? ability, int? overrideTimesTaken = null) =>
		ability == null ? string.Empty
		: ability.TimesTakeable == 1 ? ability.Name
		: $"{ability.Name} ({overrideTimesTaken ?? ability.TimesTaken}/{ability.TimesTakeable})";

	public static string DisplayText(this PlaybookSpecialAbility ability, Playbook characterPlaybook) =>
		$"{ability.LearnedTimesDisplayName(characterPlaybook.TimesTaken(ability))}: {ability.Description}";

	public static PlaybookSpecialAbility[] GetDisplayableAbilities(this GameSetting gameSetting, Playbook characterPlaybook, string abilitySourcePlaybook) =>
		gameSetting.GetPlaybookSetting(abilitySourcePlaybook)
			.GetAvailableAbilities()
			.Where(characterPlaybook.CanTakeAbility)
			.ToArray();

	public static string Display(this GearItem item) =>
		$"{item.Bulk}: {item.Name}";
}
