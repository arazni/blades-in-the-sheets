namespace Domain.Characters;

public class MonitorTrauma
{
	private readonly SortedSet<TraumaOption> traumas = new();

	public static int MaxTraumas => 4;

	public IReadOnlyCollection<TraumaOption> Traumas => this.traumas;

	public bool Add(TraumaOption trauma)
	{
		//if (trauma == TraumaOption.None)
		//	return false;

		if (IsRetired)
			return false;

		return this.traumas.Add(trauma);
	}

	public bool Remove(TraumaOption trauma) => this.traumas.Remove(trauma);

	internal bool IsRetired => this.traumas.Count == MaxTraumas;
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