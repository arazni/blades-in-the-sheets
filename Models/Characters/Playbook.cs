using Models.Settings;

namespace Models.Characters;

public class Playbook(string name)
{
	private Dictionary<string, PlaybookSpecialAbility> abilitiesByName = [];

	public IReadOnlyCollection<PlaybookSpecialAbility> Abilities => this.abilitiesByName.Values;

	public IReadOnlyDictionary<string, PlaybookSpecialAbility> AbilitiesByName
	{
		get => this.abilitiesByName;
		private set => this.abilitiesByName = value.ToDictionary(k => k.Key, v => v.Value); // json
	}

	public ExperienceTracker Experience { get; private set; } = new(8);

	public string Name { get; private set; } = name;

	public bool HasAbility(SpecialAbilitySetting setting) =>
		this.abilitiesByName.ContainsKey(setting.Name);

	public PlaybookSpecialAbility? GetAbility(SpecialAbilitySetting setting)
	{
		if (this.abilitiesByName.TryGetValue(setting.Name, out var knownAbility))
			return knownAbility;

		return null;
	}

	public bool CanTakeAbility(SpecialAbilitySetting setting)
	{
		if (this.abilitiesByName.TryGetValue(setting.Name, out var knownAbility))
			return !knownAbility.IsCompletelyLearned(setting);

		return true;
	}

	public int TimesTaken(SpecialAbilitySetting setting)
	{
		if (this.abilitiesByName.TryGetValue(setting.Name, out var knownAbility))
			return knownAbility.TimesTaken;

		return 0;
	}

	public bool TakeAbility(SpecialAbilitySetting setting)
	{
		if (this.abilitiesByName.TryGetValue(setting.Name, out var knownAbility))
			return knownAbility!.Take(setting);

		this.abilitiesByName.Add(setting.Name, new(setting));
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
