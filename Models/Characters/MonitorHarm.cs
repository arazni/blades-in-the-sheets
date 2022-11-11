using Models.Common;
using Models.Game;

namespace Models.Characters;

public class MonitorHarm
{
	private readonly BoundedList<string> lesserHarms = new(2);
	private readonly BoundedList<string> moderateHarms = new(2);
	private readonly BoundedList<string> severeHarms = new(1);
	private readonly BoundedList<string> fatalHarms = new(1);
	private readonly RolloverClock healingClock = new(4);

	public bool IsIncapacitated =>
		this.severeHarms.Any();

	public bool IsModeratelyHarmed =>
		this.moderateHarms.Any();

	public bool IsLesserHarmed =>
		this.lesserHarms.Any();

	public bool IsFatal =>
		this.fatalHarms.Any();

	public bool HasHarmAtIntensity(HarmIntensity intensity) => intensity switch
	{
		HarmIntensity.Lesser => IsLesserHarmed,
		HarmIntensity.Moderate => IsModeratelyHarmed,
		HarmIntensity.Severe => IsIncapacitated,
		HarmIntensity.Fatal => IsFatal,
		_ => throw new NotImplementedException()
	};

	public bool IsHarmed =>
		IsLesserHarmed || IsModeratelyHarmed || IsIncapacitated || IsFatal;

	public IReadOnlyCollection<HarmIntensity> AvailableIntensities
	{
		get
		{
			var available = new List<HarmIntensity>(4);

			if (!this.lesserHarms.IsFull)
				available.Add(HarmIntensity.Lesser);
			if (!this.moderateHarms.IsFull)
				available.Add(HarmIntensity.Moderate);
			if (!this.severeHarms.IsFull)
				available.Add(HarmIntensity.Severe);
			if (!this.fatalHarms.IsFull && this.severeHarms.IsFull)
				available.Add(HarmIntensity.Fatal);

			return available;
		}
	}

	public bool CanHeal => HealingClock.Ding;

	public RolloverClock HealingClock => this.healingClock;

	public IReadOnlyCollection<string> LesserHarms =>
		this.lesserHarms;

	public IReadOnlyCollection<string> ModerateHarms =>
		this.moderateHarms;

	public IReadOnlyCollection<string> SevereHarms =>
		this.severeHarms;

	public IReadOnlyCollection<string> FatalHarms =>
		this.fatalHarms;

	public IReadOnlyCollection<string> HarmsAtIntensity(HarmIntensity intensity) => intensity switch
	{
		HarmIntensity.Lesser => LesserHarms,
		HarmIntensity.Moderate => ModerateHarms,
		HarmIntensity.Severe => SevereHarms,
		HarmIntensity.Fatal => FatalHarms,
		_ => throw new NotImplementedException()
	};

	public bool AddHarm(string harmDescription, HarmIntensity intensity)
	{
		if (intensity == HarmIntensity.Lesser && this.lesserHarms.Add(harmDescription))
			return true;

		if (intensity <= HarmIntensity.Moderate && this.moderateHarms.Add(harmDescription))
			return true;

		if (intensity <= HarmIntensity.Severe && this.severeHarms.Add(harmDescription))
			return true;

		if (this.fatalHarms.Add(harmDescription))
			return true;

		return false;
	}

	public bool RemoveHarm(string harmDescription, HarmIntensity intensity) => intensity switch
	{
		HarmIntensity.Lesser => this.lesserHarms.Remove(harmDescription),
		HarmIntensity.Moderate => this.moderateHarms.Remove(harmDescription),
		HarmIntensity.Severe => this.severeHarms.Remove(harmDescription),
		HarmIntensity.Fatal => this.fatalHarms.Remove(harmDescription),
		_ => throw new NotImplementedException("Unexpected enum value reached")
	};

	public bool Heal()
	{
		if (!CanHeal)
			return false;

		HealingClock.Reset();
		this.lesserHarms.Clear();
		this.lesserHarms.AddUntilBound(this.moderateHarms);
		this.moderateHarms.Clear();
		this.moderateHarms.AddUntilBound(this.severeHarms);
		this.severeHarms.Clear();

		return true;
	}
}

public enum HarmIntensity
{
	Lesser = 1,
	Moderate = 2,
	Severe = 3,
	Fatal = 4
}
