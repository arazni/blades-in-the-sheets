namespace Models.Characters;

public class Playbook
{
	private Dictionary<string, PlaybookSpecialAbility> abilitiesByName = new();

	public Playbook(string name)
	{
		Name = name;
	}

	public IReadOnlyCollection<PlaybookSpecialAbility> Abilities => this.abilitiesByName.Values;

	public IReadOnlyDictionary<string, PlaybookSpecialAbility> AbilitiesByName
	{
		get => this.abilitiesByName;
		private set => this.abilitiesByName = value.ToDictionary(k => k.Key, v => v.Value); // json
	}

	public ExperienceTracker Experience { get; private set; } = new(8);

	public string Name { get; private set; }

	public bool TakeAbility(PlaybookSpecialAbility ability)
	{
		if (this.abilitiesByName.TryGetValue(ability.Name, out var knownAbility))
			return knownAbility!.Take();

		var copy = ability.Copy();
		copy.Take();

		this.abilitiesByName.Add(copy.Name, copy);
		return true;
	}

	public bool RemoveAbility(PlaybookSpecialAbility ability) =>
		this.abilitiesByName.Remove(ability.Name);

	public bool ClearAbilities()
	{
		if (!this.abilitiesByName.Any())
			return false;

		this.abilitiesByName.Clear();

		return true;
	}

	public static Playbook Empty() => new(string.Empty);
}
