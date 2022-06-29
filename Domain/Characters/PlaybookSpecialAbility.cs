using Domain.Common;

namespace Domain.Characters;

public class PlaybookSpecialAbility
{
	private readonly BoundedInteger timesTaken;
	private readonly Ink name;
	private readonly Ink description;

	public PlaybookSpecialAbility(string name, string description, int timesTakable, Source source)
	{
		this.name = new(name);

		this.description = new(description);

		this.timesTaken = new(Math.Max(1, timesTakable));

		Source = source;
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

	public int TimesTakable => this.timesTaken.Max;

	public Source Source { get; }
}

public enum Source
{
	Cutter = 1,
	Hound,
	Leech,
	Lurk,
	Slide,
	Spider,
	Whisper,
	Custom
}