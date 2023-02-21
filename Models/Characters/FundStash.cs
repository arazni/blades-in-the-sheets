using Models.Common;
using Models.Interactions;
using System.ComponentModel;

namespace Models.Characters;

public class FundStash : IRollable
{
	private readonly BoundedInteger stash = new(40);

	public int Stash
	{
		get => this.stash.Value;
		set => this.stash.Value = value;
	}

	public int Rating => Stash / 10;

	public bool IsFull => Stash == MaxCoins;

	public int MaxCoins => this.stash.Max;

	public int MinCoins => this.stash.Min;

	public int MaxAffordable => Stash / 2;

	public int CoinSpaceRemaining => MaxCoins - Stash;

	public Lifestyles Lifestyle => Stash <= 10 ? Lifestyles.PoorSoul
		: Stash <= 20 ? Lifestyles.Meager
		: Stash < 40 ? Lifestyles.Modest
		: Lifestyles.Fine;

	/// <returns>The number of coins left over after stowing.</returns>
	public int Stow(int coins)
	{
		if (coins < 0)
			throw new ArgumentOutOfRangeException(nameof(coins), coins, nameof(Stow));

		return this.stash.ChangeUntilBound(coins);
	}

	public bool WillFit(int coins) =>
		coins >= 0 ? this.stash.IsDeltaWithinBound(coins)
		: throw new ArgumentOutOfRangeException(nameof(coins), coins, nameof(WillFit));

	public bool Liquidate(int coins)
	{
		if (coins < 0)
			throw new ArgumentOutOfRangeException(nameof(coins), coins, nameof(Liquidate));

		return this.stash.ChangeIfWithinBound(-coins);
	}

	public bool CanLiquidate(int coins) =>
		coins >= 0 ? this.stash.IsDeltaWithinBound(-coins)
		: throw new ArgumentOutOfRangeException(nameof(coins), coins, nameof(CanLiquidate));
}

public enum Lifestyles
{
	[Description("Poor soul")]
	PoorSoul = 0,
	Meager = 1,
	Modest = 2,
	Fine = 3
}