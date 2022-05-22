namespace Domain.Characters;

public class Character
{
	public Character(GearItem[] gearItems, PlaybookSpecialAbility[] abilities, RolodexFriend[] friends)
	{
		Gear = new(gearItems);
		Playbook = new(abilities);
		Rolodex = new(friends);
	}

	public Dossier Dossier { get; private set; } = new();

	public Monitor Monitor { get; private set; } = new();

	public Talent Talent { get; private set; } = new();

	public Playbook Playbook { get; private set; }

	public Gear Gear { get; private set; }

	public Fund Fund { get; private set; } = new();

	public Rolodex Rolodex { get; private set; }

	public bool IsRetired => Monitor.Trauma.IsRetired;

	public bool IsDeadish => Monitor.Harm.IsFatal;
}
