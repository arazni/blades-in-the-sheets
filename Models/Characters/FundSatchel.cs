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

	public bool IsFull => Coins == this.coins.Max;

	public bool Spend(int amountSpent = 1)
	{
		if (amountSpent < 0)
			throw new ArgumentOutOfRangeException(nameof(amountSpent), amountSpent, nameof(Spend));

		return this.coins.ChangeIfWithinBound(-amountSpent);
	}

	public bool WillFit(int coins) =>
		this.coins.IsDeltaWithinBound(coins);

	public int Gain(int amountGained = 1)
	{
		if (amountGained < 0)
			throw new ArgumentOutOfRangeException(nameof(amountGained), amountGained, nameof(Gain));

		return this.coins.ChangeUntilBound(amountGained);
	}
}
