using Models.Settings;
using Newtonsoft.Json;

namespace Models.Characters;

public partial class Character
{
	[JsonConstructor]
	private Character()
	{
		Playbook = Playbook.Empty();
		Talent = Talent.Empty();
		Monitor = Monitor.Empty();
		GameName = string.Empty;
		Language = string.Empty;
	}

	public Character(GameSetting game, string playbookName, int version)
	{
		Playbook = new(playbookName);
		Talent = new
		(
			game.Attributes,
			game.GetPlaybookSetting(playbookName).DefaultActionPoints,
			game.ActionPointMaximum
		);
		Monitor = new(game.RecoveryClockSize);
		GameName = game.Name;
		Language = game.Language;
		Version = version;
	}

	public string Id { get; private set; } = Guid.NewGuid().ToString();

	public string GameName { get; private set; }

	public string Language { get; private set; }

	public int Version { get; private set; }

	public Dossier Dossier { get; private set; } = new();

	public Monitor Monitor { get; private set; }

	public Talent Talent { get; private set; }

	public Playbook Playbook { get; private set; }

	public Gear Gear { get; private set; } = new();

	public Fund Fund { get; private set; } = new();

	public Rolodex Rolodex { get; private set; } = new();

	public Session Session { get; private set; } = new();

	public string Notebook { get; set; } = string.Empty;

	public bool IsRetired => Monitor.Trauma.IsRetired;

	public bool IsDeadish => Monitor.Harm.IsFatal;

	public static Character Empty() => new();
}
