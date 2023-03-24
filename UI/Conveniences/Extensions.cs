using Models.Characters;

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

	public static string Display(this GearItem item) =>
		$"{item.Bulk}: {item.Name}";

	public static string PlaybookExperienceCondition(this PlaybookOption option) => option switch
	{
		PlaybookOption.Cutter => "You addressed a challenge with violence or coercion",
		PlaybookOption.Hound => "You addressed a challenge with tracking or violence",
		PlaybookOption.Leech => "You addressed a challenge with technical skill or mayhem",
		PlaybookOption.Lurk => "You addressed a challenge with stealth or evasion",
		PlaybookOption.Slide => "You addressed a challenge with deception or influence",
		PlaybookOption.Spider => "You addressed a challenge with calculation or conspiracy",
		PlaybookOption.Whisper => "You addressed a challenge with knowledge or arcane power",
		_ => "You addressed a challenge in your unique way"
	};
}
