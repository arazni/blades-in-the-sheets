using Models.Characters;
using Models.Settings;

namespace UI.Conveniences;

public static class Extensions
{
	public static string LearnedTimesDisplayName(this PlaybookSpecialAbility? ability, SpecialAbilitySetting setting) =>
		ability == null ? string.Empty
		: (setting?.TimesTakeable ?? 1) == 1 ? ability.Name
		: $"{ability.Name} ({ability?.TimesTaken ?? 0}/{setting!.TimesTakeable})";

	public static string DisplayText(this SpecialAbilitySetting setting, Playbook characterPlaybook) =>
		setting.TimesTakeable == 1 ? $"{setting.Name}: {setting.Description}"
		: $"{setting.Name} ({characterPlaybook.GetAbility(setting)?.TimesTaken ?? 0}/{setting.TimesTakeable}): {setting.Description}";

	public static SpecialAbilitySetting[] GetDisplayableAbilities(this GameSetting gameSetting, Playbook characterPlaybook, string abilitySourcePlaybook) =>
		gameSetting.GetPlaybookSetting(abilitySourcePlaybook)
			.GetAvailableAbilities()
			.Where(characterPlaybook.CanTakeAbility)
			.ToArray();

	public static string Display(this GearItem item) =>
		$"{item.Bulk}: {item.Name}";
}
