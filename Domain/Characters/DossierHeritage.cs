using System.ComponentModel;

namespace Domain.Characters;

public class DossierHeritage
{
	public Heritages Heritage { get; set; } = Heritages.Unknown;

	public string Description { get; set; } = string.Empty;
}

public enum Heritages
{
	Unknown = 0,
	Akoros,
	[Description("The Dagger Isles")]
	TheDaggerIsles,
	Iruvia,
	Severos,
	Skovlan,
	Tycheros
}