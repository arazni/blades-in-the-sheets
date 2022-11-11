namespace Models.Characters;

public class Fund
{
	public FundSatchel Satchel { get; private set; } = new();

	public FundStash Stash { get; private set; } = new();

	public bool Gain(int coins)
	{
		var remainder = Satchel.Gain(coins);

		remainder = Stash.Stow(remainder);

		return remainder == 0;
	}

	public bool Spend(int coins) =>
		Satchel.Spend(coins);

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