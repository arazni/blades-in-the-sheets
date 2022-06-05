namespace Domain.Characters;

public class Character
{
	private Character()
	{
		Playbook = new(PlaybookOption.Unknown);
		Talent = new(PlaybookOption.Unknown);
	}

	public Character(PlaybookOption option)
	{
		Playbook = new(option);
		Talent = new(option);
	}

	public string Id { get; private set; } = Guid.NewGuid().ToString();

	public Dossier Dossier { get; private set; } = new();

	public Monitor Monitor { get; private set; } = new();

	public Talent Talent { get; private set; }

	public Playbook Playbook { get; private set; }

	public Gear Gear { get; private set; } = new();

	public Fund Fund { get; private set; } = new();

	public Rolodex Rolodex { get; private set; } = new();

	public bool IsRetired => Monitor.Trauma.IsRetired;

	public bool IsDeadish => Monitor.Harm.IsFatal;

	public static Character Empty() => new();
}
