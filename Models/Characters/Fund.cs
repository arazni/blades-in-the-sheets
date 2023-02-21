namespace Models.Characters;

public class Fund
{
	public FundSatchel Satchel { get; private set; } = new();

	public FundStash Stash { get; private set; } = new();

	public int MaxAffordable => Satchel.MaxAffordable + Stash.MaxAffordable;

	public int MinCoins => Satchel.MinCoins + Stash.MinCoins;

	public int CoinSpaceRemaining => Satchel.CoinSpaceRemaining + Stash.CoinSpaceRemaining;

	public bool Gain(int coins)
	{
		var remainder = Satchel.Gain(coins);

		remainder = Stash.Stow(remainder);

		return remainder == 0;
	}

	public bool CanGain(int coins) => CoinSpaceRemaining >= coins;

	public bool CanSpend(int coins) => MaxAffordable >= coins;

	public bool Spend(int coins)
	{
		if (coins > MaxAffordable)
			return false;

		var remainder = Satchel.SpendAsMuchAsAffordable(coins);

		return Stash.Liquidate(remainder * 2);
	}

	public bool Liquidate(int coins = 1)
	{
		if (Satchel.IsFull)
			return false;

		if (!Satchel.WillFit(coins))
			return false;

		if (!Stash.Liquidate(coins * 2))
			return false;

		Satchel.Gain(coins);

		return true;
	}

	public Lifestyles Lifestyle => Stash.Lifestyle;
}