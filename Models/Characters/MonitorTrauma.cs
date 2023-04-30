using Newtonsoft.Json;

namespace Models.Characters;

public class MonitorTrauma
{
	[JsonProperty]
	private SortedSet<string> TraumaSet { get; set; } = new();

	public static int MaxTraumas => 4;

	public IReadOnlyCollection<string> Traumas => TraumaSet;

	public bool Add(string trauma)
	{
		if (IsRetired)
			return false;

		return TraumaSet.Add(trauma);
	}

	public bool Remove(string trauma) => TraumaSet.Remove(trauma);

	internal bool IsRetired => Traumas.Count == MaxTraumas;
}
