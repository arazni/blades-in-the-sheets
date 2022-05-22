namespace Domain.Characters;

public class GearItem
{
	public GearItem(int bulk, string name, Sources source)
	{
		Bulk = bulk;
		Name = name;
		Source = source;
	}

	public int Bulk { get; }

	public string Name { get; }

	public Sources Source { get; }

	public enum Sources
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
