using Persistence.Json.DataModels;

namespace UI;

public static class Constants
{
	public static class Classes
	{
		public static string DisplayNone(bool isDisplayNone) => isDisplayNone ? "dn" : string.Empty;

		public static string Display(bool isDisplay) => isDisplay ? string.Empty : "dn";
	}

	public static class Paths
	{
		private const string New = "new";

		public const string Sheets = "sheets";

		public const string Tips = "tip-jar";

		public const string Theme = "theme";

		public const string Accessibility = "accessibility";

		public const string Help = "help";

		public static string NewCharacter(GameFile gameFile) => NewCharacter(gameFile.Stem);

		public static string NewCharacter(string gameFileStem) => $"{New}/{gameFileStem}";

		public static string NewCharacter(string gameFileStem, string playbookName) => $"{NewCharacter(gameFileStem)}/{playbookName}";

		public static string Sheet(string id) => $"{Sheets}/{id}";
	}
}