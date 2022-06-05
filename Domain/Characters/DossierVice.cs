namespace Domain.Characters;

public class DossierVice
{
	public ViceOption Vice { get; set; } = ViceOption.Unknown;

	public string Description { get; set; } = string.Empty;
}

public enum ViceOption
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