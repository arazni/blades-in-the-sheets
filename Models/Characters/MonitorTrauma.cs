using Newtonsoft.Json;

namespace Models.Characters;

public class MonitorTrauma
{
	[JsonProperty]
	private SortedSet<TraumaOption> TraumaSet { get; set; } = new();

	public static int MaxTraumas => 4;

	public IReadOnlyCollection<TraumaOption> Traumas => TraumaSet;

	public bool Add(TraumaOption trauma)
	{
		//if (trauma == TraumaOption.None)
		//	return false;

		if (IsRetired)
			return false;

		return TraumaSet.Add(trauma);
	}

	public bool Remove(TraumaOption trauma) => TraumaSet.Remove(trauma);

	internal bool IsRetired => Traumas.Count == MaxTraumas;
}

public enum TraumaOption
{
	//None,
	Cold,
	Haunted,
	Obsessed,
	Paranoid,
	Reckless,
	Soft,
	Unstable,
	Vicious
}