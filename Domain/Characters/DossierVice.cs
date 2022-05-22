namespace Domain.Characters;

public class DossierVice
{
	public Vices Vice { get; set; } = Vices.Unknown;

	public string Description { get; set; } = string.Empty;
}

public enum Vices
{
	Unknown = 0,
	Faith,
	Gambling,
	Luxury,
	Obligation,
	Pleasure,
	Stupor,
	Weird
}