using Domain.Characters;

namespace UI.Conveniences;

public static class Extensions
{
	public static string DisplayName(this HeritageOption option) => option switch
	{
		HeritageOption.TheDaggerIsles => "The Dagger Isles",
		_ => option.ToString()
	};

	public static string LearnedTimesDisplayName(this PlaybookSpecialAbility? ability) =>
		ability == null ? string.Empty
		: ability.TimesTakable == 1 ? ability.Name
		: $"{ability.Name} ({ability.TimesTaken}/{ability.TimesTakable})";
}
