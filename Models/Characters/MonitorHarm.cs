using Models.Common;
using Models.Game;
using Newtonsoft.Json;

namespace Models.Characters;

public class MonitorHarm
{
	[JsonProperty]
	private BoundedCollection<string> BoundedLesserHarms { get; set; } = new(2);
	[JsonProperty]
	private BoundedCollection<string> BoundedModerateHarms { get; set; } = new(2);
	[JsonProperty]
	private BoundedCollection<string> BoundedSevereHarms { get; set; } = new(1);
	[JsonProperty]
	private BoundedCollection<string> BoundedFatalHarms { get; set; } = new(1);

	public RolloverClock HealingClock { get; private set; } = new(4);

	public bool IsIncapacitated =>
		BoundedSevereHarms.Any();

	public bool IsModeratelyHarmed =>
		BoundedModerateHarms.Any();

	public bool IsLesserHarmed =>
		BoundedLesserHarms.Any();

	public bool IsFatal =>
		BoundedFatalHarms.Any();

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

			if (!BoundedLesserHarms.IsFull)
				available.Add(HarmIntensity.Lesser);
			if (!BoundedModerateHarms.IsFull)
				available.Add(HarmIntensity.Moderate);
			if (!BoundedSevereHarms.IsFull)
				available.Add(HarmIntensity.Severe);
			if (!BoundedFatalHarms.IsFull && BoundedSevereHarms.IsFull)
				available.Add(HarmIntensity.Fatal);

			return available;
		}
	}

	public bool CanHeal => HealingClock.Ding;

	public IReadOnlyCollection<string> LesserHarms =>
		BoundedLesserHarms;

	public IReadOnlyCollection<string> ModerateHarms =>
		BoundedModerateHarms;

	public IReadOnlyCollection<string> SevereHarms =>
		BoundedSevereHarms;

	public IReadOnlyCollection<string> FatalHarms =>
		BoundedFatalHarms;

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
		if (intensity == HarmIntensity.Lesser && BoundedLesserHarms.Add(harmDescription))
			return true;

		if (intensity <= HarmIntensity.Moderate && BoundedModerateHarms.Add(harmDescription))
			return true;

		if (intensity <= HarmIntensity.Severe && BoundedSevereHarms.Add(harmDescription))
			return true;

		if (BoundedFatalHarms.Add(harmDescription))
			return true;

		return false;
	}

	public bool RemoveHarm(string harmDescription, HarmIntensity intensity) => intensity switch
	{
		HarmIntensity.Lesser => BoundedLesserHarms.Remove(harmDescription),
		HarmIntensity.Moderate => BoundedModerateHarms.Remove(harmDescription),
		HarmIntensity.Severe => BoundedSevereHarms.Remove(harmDescription),
		HarmIntensity.Fatal => BoundedFatalHarms.Remove(harmDescription),
		_ => throw new NotImplementedException("Unexpected enum value reached")
	};

	public bool Heal()
	{
		if (!CanHeal)
			return false;

		HealingClock.Reset();
		BoundedLesserHarms.Clear();
		BoundedLesserHarms.AddUntilBound(BoundedModerateHarms);
		BoundedModerateHarms.Clear();
		BoundedModerateHarms.AddUntilBound(BoundedSevereHarms);
		BoundedSevereHarms.Clear();

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
