namespace Models.Characters;

public partial class Character // Bulk
{
	public int AvailableBulk => Gear.AvailableBulk - Fund.Satchel.Coins;

	public bool CanCommitGear(GearItem item)
	{
		if (item.Bulk > AvailableBulk)
			return false;

		return Gear.CanCommitGear(item);
	}

	public bool CommitGear(GearItem item)
	{
		if (!CanCommitGear(item))
			return false;

		return Gear.CommitGear(item);
	}

	public bool UncommitGear(GearItem item) => Gear.UncommitGear(item);

	public bool ClearCommitments() => Gear.ClearCommitments();
}
