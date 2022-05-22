namespace Imports;

internal class AbilityData
{
	private string name = string.Empty;
	private string description = string.Empty;

	public int TimesTakeable { get; set; }

	public string Name { get => this.name; set => this.name = value.Trim(); }

	public string Description { get => this.description; set => this.description = value.Trim(); }
}
