namespace Models.Characters;

public class Playbook
{
	private Dictionary<string, PlaybookSpecialAbility> abilitiesByName = new();

	public Playbook(PlaybookOption option)
	{
		Option = option;
	}

	public IReadOnlyCollection<PlaybookSpecialAbility> Abilities => this.abilitiesByName.Values;

	public IReadOnlyDictionary<string, PlaybookSpecialAbility> AbilitiesByName
	{
		get => this.abilitiesByName;
		private set { this.abilitiesByName = value.ToDictionary(k => k.Key, v => v.Value); } // json
	}

	public ExperienceTracker Experience { get; } = new(8);

	public PlaybookOption Option { get; }

	public bool TakeAbility(PlaybookSpecialAbility ability)
	{
		var isKnown = this.abilitiesByName.TryGetValue(ability.Name, out var knownAbility);

		if (isKnown)
			return knownAbility!.Take();

		var copy = ability.Copy();
		copy.Take();

		this.abilitiesByName.Add(copy.Name, copy);
		return true;
	}

	public bool RemoveAbility(PlaybookSpecialAbility ability)
	{
		if (!this.abilitiesByName.ContainsKey(ability.Name))
			return false;

		this.abilitiesByName.Remove(ability.Name);
		return true;
	}

	public bool ClearAbilities()
	{
		if (!this.abilitiesByName.Any())
			return false;

		this.abilitiesByName.Clear();

		return true;
	}

	public static Playbook Empty() => new(PlaybookOption.Unknown);
}

public enum PlaybookOption
{
	Unknown = -1,
	Custom = 0,
	Cutter = 1,
	Hound,
	Leech,
	Lurk,
	Slide,
	Spider,
	Whisper
}