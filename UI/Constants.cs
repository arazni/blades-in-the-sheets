namespace UI;

public static class Constants
{
	public static class Classes
	{
		public static string DisplayNone(bool isDisplayNone) => isDisplayNone ? "d-none" : string.Empty;

		public static string Display(bool isDisplay) => isDisplay ? string.Empty : "d-none";
	}

	public static class Paths
	{
		public const string New = "new";

		public const string Characters = "characters";

		public const string Sheets = "sheets";

		public static string Character(string id) => $"{Characters}/{id}";

		public static string Sheet(string id) => $"{Sheets}/{id}";
	}

	public static class Buttons
	{
		public const string MinWidth = "125px";

		public const string MinHeight = "54px";

		public static string ButtonDimensionStyle => $"min-width: {MinWidth}; min-height: {MinHeight};";
	}
}