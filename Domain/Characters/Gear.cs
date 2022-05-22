using System.Collections.Immutable;

namespace Domain.Characters;

public class Gear
{
	private readonly List<GearItem> loadout = new();

	public Gear(GearItem[] availableItems)
	{
		AvailableItems = availableItems.ToImmutableDictionary(gi => gi.Name, StringComparer.InvariantCultureIgnoreCase);
	}

	public IReadOnlyDictionary<string, GearItem> AvailableItems { get; }

	public GearCommitment Commitment { get; } = new();

	public IReadOnlyCollection<GearItem> Loadout => this.loadout;

	public int MaxBulk => Commitment.MaxBulk;

	public int AvailableBulk => Commitment.MaxBulk - Loadout.Sum(i => i.Bulk);

	public bool AddGear(string name)
	{
		AvailableItems.TryGetValue(name, out GearItem? item);

		if (item == null)
			return false;

		if (item.Bulk > AvailableBulk)
			return false;

		this.loadout.Add(item);

		return true;
	}

	public bool RemoveGear(string name)
	{
		AvailableItems.TryGetValue(name, out GearItem? item);

		if (item == null)
			return false;

		this.loadout.Remove(item);

		return true;
	}

	public bool ClearGear()
	{
		if (!this.loadout.Any())
			return false;

		this.loadout.Clear();

		return true;
	}
}
