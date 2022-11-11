using System.ComponentModel;

namespace Models.Characters;

public class DossierHeritage
{
	public HeritageOption Heritage { get; set; } = HeritageOption.Unknown;

	public string Description { get; set; } = string.Empty;
}

public enum HeritageOption
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