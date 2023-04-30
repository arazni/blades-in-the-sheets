using Models.Characters;

namespace UI.Conveniences;

public static class Extensions
{
	public static string LearnedTimesDisplayName(this PlaybookSpecialAbility? ability) =>
		ability == null ? string.Empty
		: ability.TimesTakeable == 1 ? ability.Name
		: $"{ability.Name} ({ability.TimesTaken}/{ability.TimesTakeable})";

	public static string Display(this GearItem item) =>
		$"{item.Bulk}: {item.Name}";
}
