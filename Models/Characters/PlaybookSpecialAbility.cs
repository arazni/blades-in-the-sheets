using Models.Common;
using Models.Settings;
using Newtonsoft.Json;

namespace Models.Characters;

public class PlaybookSpecialAbility
{
	private readonly Ink name;
	private readonly Ink description;

	[JsonConstructor]
	protected PlaybookSpecialAbility(string name, string description, int timesTaken)
	{
		this.name = new(name);
		this.description = new(description);
		TimesTaken = timesTaken;
	}

	public PlaybookSpecialAbility(string name, string description)
	{
		this.name = new(name);
		this.description = new(description);
		TimesTaken = 1;
	}

	public PlaybookSpecialAbility(SpecialAbilitySetting setting)
	{
		this.name = new(setting.Name);
		this.description = new(setting.Description);
		TimesTaken = 1;
	}

	public bool Take(SpecialAbilitySetting setting)
	{
		if (!setting.Name.Like(Name))
			return false;

		if (setting.TimesTakeable <= TimesTaken)
			return false;

		TimesTaken++;
		return true;
	}

	public string Name
	{
		get => this.name.Value;
		private set => this.name.Value = value;
	}

	public string Description
	{
		get => this.description.Value;
		private set => this.description.Value = value;
	}

	public int TimesTaken { get; private set; }

	public bool OverwriteDescription(string description)
	{
		if (!description.HasInk())
			return false;

		Description = description;
		return true;
	}

	public bool IsCompletelyLearned(SpecialAbilitySetting setting)
	{
		if (!setting.Name.Like(Name))
			throw new ArgumentException("Ensure the setting matches the playbook ability", nameof(setting));

		return setting.TimesTakeable <= TimesTaken;
	}
}
