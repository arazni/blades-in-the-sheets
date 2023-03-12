using Models.Common;

namespace Models.Characters;

public class FundSatchel
{
	private readonly BoundedInteger coins = new(4);

	public int Coins
	{
		get => this.coins.Value;
		set
		{
			var anyDifference = this.coins.Value != value;

			this.coins.Value = value;

			if (anyDifference)
				NotifySatchelChanged();
		}
	}

	public bool IsFull => Coins == MaxCoins;

	public int MaxCoins => this.coins.Max;

	public int MinCoins => this.coins.Min;

	public int MaxAffordable => Coins;

	public int CoinSpaceRemaining => MaxCoins - Coins;

	internal bool SpendIfAffordable(int amountSpent = 1)
	{
		if (amountSpent < 0)
			throw new ArgumentOutOfRangeException(nameof(amountSpent), amountSpent, nameof(SpendIfAffordable));

		if (amountSpent == 0 || Coins == 0)
			return false;

		var hasChanged = this.coins.ChangeIfWithinBound(-amountSpent);

		if (hasChanged)
			NotifySatchelChanged();

		return hasChanged;
	}

	internal int SpendAsMuchAsAffordable(int amountSpent)
	{
		if (amountSpent < 0)
			throw new ArgumentOutOfRangeException(nameof(amountSpent), amountSpent, nameof(SpendAsMuchAsAffordable));

		if (amountSpent == 0)
			return 0;

		if (Coins == 0)
			return amountSpent;

		var remainder = Math.Abs(this.coins.ChangeUntilBound(-amountSpent));

		NotifySatchelChanged();

		return remainder;
	}

	public bool WillFit(int coins) =>
		this.coins.IsDeltaWithinBound(coins);

	internal int Gain(int amountGained = 1)
	{
		if (amountGained < 0)
			throw new ArgumentOutOfRangeException(nameof(amountGained), amountGained, nameof(Gain));

		if (amountGained == 0)
			return 0;

		if (Coins == MaxCoins)
			return amountGained;

		var remainder = Math.Abs(this.coins.ChangeUntilBound(amountGained));

		NotifySatchelChanged();

		return remainder;
	}

	public event Action? SatchelChanged;
	public void NotifySatchelChanged() => SatchelChanged?.Invoke();
}
