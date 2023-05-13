using Models.Common;

namespace Models.Characters;

public class GearItem
{
	private readonly Ink name;

	public GearItem(int bulk, string name)
	{
		Bulk = bulk;
		this.name = new(name);
	}

	public int Bulk { get; }

	public string Name
	{
		get => this.name.Value;
		internal set => this.name.Value = value;
	}
}
