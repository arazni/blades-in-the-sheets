namespace UI;

public static class Constants
{
	public static class Paths
	{
		public const string Characters = "/characters";

		public static string Character(string id) => $"{Characters}/{id}";
	}
}