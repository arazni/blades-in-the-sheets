namespace Models.Characters;

public class Dossier
{
	public string Name { get; set; } = string.Empty;

	public string CrewId { get; set; } = string.Empty;

	public string Alias { get; set; } = string.Empty;

	public string Look { get; set; } = string.Empty;

	public string Notes { get; set; } = string.Empty;

	public DossierBackground Background { get; set; } = new();

	public DossierHeritage Heritage { get; set; } = new();

	public DossierVice Vice { get; set; } = new();
}