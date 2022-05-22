using System.Collections.Immutable;

namespace Domain.Characters;

public class Playbook
{
	private readonly List<PlaybookSpecialAbility> abilities = new();

	public Playbook(PlaybookSpecialAbility[] abilities)
	{
		AvailableAbilities = abilities.ToImmutableDictionary(a => a.Name, StringComparer.InvariantCultureIgnoreCase);
	}

	public IReadOnlyDictionary<string, PlaybookSpecialAbility> AvailableAbilities { get; }

	public IReadOnlyCollection<PlaybookSpecialAbility> Abilities => this.abilities;

	public ExperienceTracker Experience { get; } = new(8);

	public bool TakeAbility(string name)
	{
		AvailableAbilities.TryGetValue(name, out PlaybookSpecialAbility? ability);

		if (ability == null)
			return false;

		this.abilities.Add(ability);

		return true;
	}

	public bool RemoveAbility(string name)
	{
		AvailableAbilities.TryGetValue(name, out PlaybookSpecialAbility? ability);

		if (ability == null)
			return false;

		this.abilities.Remove(ability);

		return true;
	}

	public bool ClearAbilities()
	{
		if (!this.abilities.Any())
			return false;

		this.abilities.Clear();

		return true;
	}
}

public enum Playbooks
{
	Cutter = 1,
	Hound,
	Leech,
	Lurk,
	Slide,
	Spider,
	Whisper
}