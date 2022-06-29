using Domain.Characters;

namespace UI.Conveniences;

public static class Extensions
{
	public static string DisplayName(this HeritageOption option) => option switch
	{
		HeritageOption.TheDaggerIsles => "The Dagger Isles",
		_ => option.ToString()
	};
}
