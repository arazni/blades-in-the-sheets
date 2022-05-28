namespace Domain.Characters;

public class Gear
{
	private readonly Dictionary<string, GearItem> loadoutByName = new(StringComparer.InvariantCultureIgnoreCase);

	public GearCommitment Commitment { get; } = new();

	public IReadOnlyCollection<GearItem> Loadout => this.loadoutByName.Values;

	public int MaxBulk => Commitment.MaxBulk;

	public int AvailableBulk => Commitment.MaxBulk - Loadout.Sum(i => i.Bulk);

	public bool AddGear(GearItem item)
	{
		if (item.Bulk > AvailableBulk)
			return false;

		if (this.loadoutByName.ContainsKey(item.Name))
			return false;

		this.loadoutByName.Add(item.Name, item);
		return true;
	}

	public bool RemoveGear(GearItem item)
	{
		if (!this.loadoutByName.ContainsKey(item.Name))
			return false;

		this.loadoutByName.Remove(item.Name);
		return true;
	}

	public bool ClearGear()
	{
		if (!this.loadoutByName.Any())
			return false;

		this.loadoutByName.Clear();

		return true;
	}
}
