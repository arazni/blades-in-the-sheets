using Models.Common;

namespace Models.Characters;

public class PlaybookSpecialAbility
{
	private readonly BoundedInteger timesTaken;
	private readonly Ink name;
	private readonly Ink description;

	public PlaybookSpecialAbility(string name, string description, int timesTakeable)
	{
		this.name = new(name);

		this.description = new(description);

		this.timesTaken = new(Math.Max(1, timesTakeable));
	}

	public bool Take() => TimesTaken != ++TimesTaken;

	public bool Cancel() => TimesTaken != --TimesTaken;

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

	public int TimesTaken
	{
		get => this.timesTaken.Value;
		private set => this.timesTaken.Value = value;
	}

	public bool OverwriteDescription(string description)
	{
		if (!description.HasInk())
			return false;

		Description = description;
		return true;
	}

	public int TimesTakeable => this.timesTaken.Max;

	public bool IsCompletelyLearned => TimesTakeable == TimesTaken;

	public PlaybookSpecialAbility Copy() =>
		new(Name, Description, TimesTakeable);
}
