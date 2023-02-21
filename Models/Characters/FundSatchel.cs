using Models.Common;

namespace Models.Characters;

public class FundSatchel
{
	private readonly BoundedInteger coins = new(4);

	public int Coins
	{
		get => this.coins.Value;
		set => this.coins.Value = value;
	}

	public bool IsFull => Coins == MaxCoins;

	public int MaxCoins => this.coins.Max;

	public int MinCoins => this.coins.Min;

	public int MaxAffordable => Coins;

	public int CoinSpaceRemaining => MaxCoins - Coins;

	public bool SpendIfAffordable(int amountSpent = 1)
	{
		if (amountSpent < 0)
			throw new ArgumentOutOfRangeException(nameof(amountSpent), amountSpent, nameof(SpendIfAffordable));

		return this.coins.ChangeIfWithinBound(-amountSpent);
	}

	public int SpendAsMuchAsAffordable(int amountSpent)
	{
		if (amountSpent < 0)
			throw new ArgumentOutOfRangeException(nameof(amountSpent), amountSpent, nameof(SpendIfAffordable));

		return Math.Abs(this.coins.ChangeUntilBound(-amountSpent));
	}

	public bool WillFit(int coins) =>
		this.coins.IsDeltaWithinBound(coins);

	public int Gain(int amountGained = 1)
	{
		if (amountGained < 0)
			throw new ArgumentOutOfRangeException(nameof(amountGained), amountGained, nameof(Gain));

		return Math.Abs(this.coins.ChangeUntilBound(amountGained));
	}
}
