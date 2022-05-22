namespace Domain.Characters;

public class DossierBackground
{
	public Backgrounds Background { get; set; } = Backgrounds.Unknown;

	public string Description { get; set; } = string.Empty;
}

public enum Backgrounds
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
