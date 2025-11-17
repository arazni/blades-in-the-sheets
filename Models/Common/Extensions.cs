using System.ComponentModel;
using System.Text;

namespace Models.Common;

public static class Extensions
{
	public static bool Like(this string source, string comparison) =>
		string.Equals(source.Trim(), comparison.Trim(), StringComparison.InvariantCultureIgnoreCase);

	public static bool Embeds(this string source, string smallerString) =>
		source.Contains(smallerString.Trim(), StringComparison.InvariantCultureIgnoreCase);

	public static bool EmbedsAny(this string source, params string[] smallerStrings) =>
		smallerStrings.Any(source.Embeds);

	public static bool In(this string source, params string[] list) =>
		list.Any(source.Like);

	public static bool In(this string source, IEnumerable<string> list) =>
		list.Any(source.Like);

	public static bool In<T>(this T source, params T[] list) =>
		list.Contains(source);

	public static bool EndsIn(this string source, params string[] list) =>
		list.Any(l => source.EndsWith(l, StringComparison.InvariantCultureIgnoreCase));

	public static bool Has(this IEnumerable<string> source, string item) =>
		item.In(source);

	public static bool HasAny(this IEnumerable<string> source, params string[] items) =>
		items.Any(source.Has);

	public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable) =>
		enumerable == null || !enumerable.Any();

	public static bool HasInk(this string? source) =>
		!string.IsNullOrWhiteSpace(source);

	public static string Join(this IEnumerable<string> source, string separator) =>
		string.Join(separator, source) ?? string.Empty;

	public static string Join<T>(this IEnumerable<T> source, string separator) where T : notnull =>
		string.Join(separator, source.Select(s => s.ToString()));

	public static string Description<T>(this T option) where T : Enum
	{
		var description = typeof(T)
			.GetField(option.ToString())
			!.GetCustomAttributes(typeof(DescriptionAttribute), true)
			as DescriptionAttribute[];

		if (description != null && description.Any())
			return description.First().Description;

		return option.ToString();
	}

	public static string ExceptionDetails(this Exception exception)
	{
		StringBuilder message = new(exception.Message);
		message.AppendLine(exception.StackTrace);
		var inner = exception.InnerException;

		while (inner != null)
		{
			message.AppendLine("INNER: ");
			message.AppendLine(inner.Message);
			message.AppendLine(inner.StackTrace);
			inner = inner.InnerException;
		}

		return message.ToString();
	}
}
