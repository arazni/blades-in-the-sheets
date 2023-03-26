using Models.Common;
using Newtonsoft.Json;

namespace Models.Characters;

public class Gear
{
	[JsonProperty]
	private Dictionary<string, GearItem> LoadoutByName { get; set; } = new(StringComparer.InvariantCultureIgnoreCase);
	[JsonProperty]
	private Dictionary<string, GearItem> AvailableGearByName { get; set; } = new(StringComparer.InvariantCultureIgnoreCase);

	private const string Bandolier = "bandolier";

	public GearCommitment Commitment { get; } = new();

	public IReadOnlyCollection<GearItem> Loadout => LoadoutByName.Values;

	public IReadOnlyCollection<GearItem> AvailableGear => AvailableGearByName.Values;

	public IReadOnlyCollection<GearItem> UncommittedGear => AvailableGearByName.Where(available => !LoadoutByName.ContainsKey(available.Key))
		.Select(uncommitted => uncommitted.Value)
		.ToArray();

	public int MaxBulk => Commitment.MaxBulk;

	internal int AvailableBulk => Commitment.MaxBulk - Loadout.Sum(i => i.Bulk);

	public bool CanAddAvailableItem(string itemName) => !AvailableGearByName.ContainsKey(itemName);

	public bool CanAddAvailableItem(GearItem item) => CanAddAvailableItem(item.Name);

	public bool AddAvailableItem(GearItem item)
	{
		if (!CanAddAvailableItem(item))
			return false;

		AvailableGearByName.Add(item.Name, item);
		return true;
	}

	public bool RemoveAvailableItem(GearItem item)
	{
		if (!AvailableGearByName.ContainsKey(item.Name))
			return false;

		AvailableGearByName.Remove(item.Name);
		LoadoutByName.Remove(item.Name);

		return true;
	}

	public bool ClearAvailableItems()
	{
		if (!AvailableGearByName.Any())
			return false;

		ClearCommitments();
		AvailableGearByName.Clear();
		return true;
	}

	internal bool CanCommitGear(GearItem item)
	{
		if (Commitment.Commitment == LoadCommitmentOption.None)
			return false;

		if (!AvailableGearByName.ContainsKey(item.Name))
			return false;

		if (item.Bulk > AvailableBulk)
			return false;

		if (LoadoutByName.ContainsKey(item.Name))
			return false;

		return true;
	}

	public bool HasCommitted() => Commitment.Commitment != LoadCommitmentOption.None;

	internal bool CommitGear(GearItem item)
	{
		if (!CanCommitGear(item))
			return false;

		LoadoutByName.Add(item.Name, item);
		return true;
	}

	internal bool UncommitGear(GearItem item)
	{
		if (!AvailableGearByName.ContainsKey(item.Name))
			return false;

		return LoadoutByName.Remove(item.Name);
	}

	internal bool ClearCommitments()
	{
		if (!LoadoutByName.Any())
			return false;

		LoadoutByName.Clear();
		Commitment.Commitment = LoadCommitmentOption.None;
		return true;
	}

	public bool HasCommittedBandolier => Loadout.Any(x => x.Name.Embeds(Bandolier));
}
