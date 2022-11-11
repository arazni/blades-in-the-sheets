namespace Models.Common;

public class BoundedInteger
{
	private int value;

	public BoundedInteger(int max)
	{
		Min = 0;
		Max = max;
		Value = Min;
	}

	public BoundedInteger(int min, int max)
	{
		Min = min;
		Max = max;
		Value = Min;
	}

	public int Value
	{
		get => this.value;
		set => this.value = Math.Clamp(value, Min, Max);
	}

	public int Max { get; set; }

	public int Min { get; set; }

	public int Remainder(int delta)
	{
		if (delta > 0 && delta + Value > Max)
			return this.value + delta - Max;

		if (delta < 0 && delta + Value < Min)
			return this.value + delta - Min;

		return 0;
	}

	/// <returns>The remainder beyond the defined boundaries</returns>
	public int ChangeUntilBound(int delta)
	{
		var remainder = Remainder(delta);

		Value += delta;
		return remainder;
	}

	public bool ChangeIfWithinBound(int delta)
	{
		if (Remainder(delta) != 0)
			return false;

		Value += delta;

		return true;
	}

	public bool IsDeltaWithinBound(int delta) =>
		Remainder(delta) == 0;
}
