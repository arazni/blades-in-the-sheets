using Persistence.Json.DataModels;

namespace UI;

public static class Constants
{
	public static class Classes
	{
		public static string DisplayNone(bool isDisplayNone) => isDisplayNone ? "display: none;" : string.Empty;

		public static string Display(bool isDisplay) => isDisplay ? string.Empty : "display: none;";
	}

	public static class Paths
	{
		private const string New = "new";

		public const string Sheets = "sheets";

		public const string Tips = "tip-jar";

		public static string NewCharacter(GameFile gameFile) => NewCharacter(gameFile.Stem);

		public static string NewCharacter(string gameFileStem) => $"{New}/{gameFileStem}";

		public static string NewCharacter(string gameFileStem, string playbookName) => $"{NewCharacter(gameFileStem)}/{playbookName}";

		public static string Sheet(string id) => $"{Sheets}/{id}";
	}

	public static class Buttons
	{
		public const string MinWidth = "125px";

		public const string MinHeight = "54px";

		public static string ButtonDimensionStyle => $"min-width: {MinWidth}; min-height: {MinHeight};";
	}
}