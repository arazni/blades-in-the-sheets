namespace Domain.Common;

public static class Extensions
{
	public static bool Like(this string source, string comparison) =>
		string.Equals(source.Trim(), comparison.Trim(), StringComparison.InvariantCultureIgnoreCase);

	public static bool Embeds(this string source, string smallerString) =>
		source.Contains(smallerString.Trim(), StringComparison.InvariantCultureIgnoreCase);

	public static bool In(this string source, params string[] list) =>
		list.Any(item => source.Like(item));

	public static bool In(this string source, IEnumerable<string> list) =>
		list.Any(item => source.Like(item));

	public static bool Has(this IEnumerable<string> source, string item) =>
		item.In(source);
}
