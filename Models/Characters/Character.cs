using Models.Common;
using Models.Settings;

namespace Models.Characters;

public partial class Character
{
	private Character() { }

	public Character(GameSetting game, string playbookName)
	{
		Playbook = new(playbookName);
		Talent = new
		(
			game.Attributes,
			game.Playbooks.First(playbookSetting => playbookSetting.Name.Like(playbookName)).DefaultActionPoints,
			game.ActionPointMaximum
		);
	}

	public string Id { get; private set; } = Guid.NewGuid().ToString();

	public string GameName { get; private set; } = Constants.Games.BladesInTheDark;

	public Dossier Dossier { get; private set; } = new();

	public Monitor Monitor { get; private set; } = new();

	public Talent Talent { get; private set; } = Talent.Empty();

	public Playbook Playbook { get; private set; } = Playbook.Empty();

	public Gear Gear { get; private set; } = new();

	public Fund Fund { get; private set; } = new();

	public Rolodex Rolodex { get; private set; } = new();

	public Session Session { get; private set; } = new();

	public string Notebook { get; set; } = string.Empty;

	public bool IsRetired => Monitor.Trauma.IsRetired;

	public bool IsDeadish => Monitor.Harm.IsFatal;

	public static Character Empty() => new();
}
