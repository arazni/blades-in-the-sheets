namespace Models.Characters;

public class DossierBackground
{
	public BackgroundOption Background { get; set; } = BackgroundOption.Unknown;

	public string Description { get; set; } = string.Empty;
}

public enum BackgroundOption
{
	Unknown,
	Academic,
	Labor,
	Law,
	Trade,
	Military,
	Noble,
	Underworld
}
