using Models.Common;

namespace Models.Characters;

public class GearItem
{
	private readonly Ink name;

	public GearItem(int bulk, string name, SourceOption source)
	{
		Bulk = bulk;
		this.name = new(name);
		Source = source;
	}

	public int Bulk { get; }

	public string Name
	{
		get => this.name.Value;
		internal set => this.name.Value = value;
	}

	public SourceOption Source { get; }

	public enum SourceOption
	{
		Standard,
		Cutter,
		Hound,
		Leech,
		Lurk,
		Slide,
		Spider,
		Whisper,
		Custom
	}
}
