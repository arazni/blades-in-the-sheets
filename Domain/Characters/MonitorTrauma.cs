namespace Domain.Characters;

public class MonitorTrauma
{
	private readonly SortedSet<Traumas> traumas = new();

	public static int MaxTraumas => 4;

	public IReadOnlyCollection<Traumas> Traumas => this.traumas;

	public bool Add(Traumas trauma)
	{
		if (IsRetired)
			return false;

		return this.traumas.Add(trauma);
	}

	public bool Remove(Traumas trauma) => this.traumas.Remove(trauma);

	internal bool IsRetired => this.traumas.Count == MaxTraumas;
}

public enum Traumas
{
	None,
	Cold,
	Haunted,
	Obsessed,
	Paranoid,
	Reckless,
	Soft,
	Unstable,
	Vicious
}