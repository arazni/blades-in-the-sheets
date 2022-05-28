namespace Domain.Characters;

public class Character
{
	public Character(PlaybookOption option)
	{
		Playbook = new Playbook(option);
	}

	public Guid Id { get; } = Guid.NewGuid();

	public Dossier Dossier { get; private set; } = new();

	public Monitor Monitor { get; private set; } = new();

	public Talent Talent { get; private set; } = new();

	public Playbook Playbook { get; private set; }

	public Gear Gear { get; private set; } = new();

	public Fund Fund { get; private set; } = new();

	public Rolodex Rolodex { get; private set; } = new();

	public bool IsRetired => Monitor.Trauma.IsRetired;

	public bool IsDeadish => Monitor.Harm.IsFatal;
}
