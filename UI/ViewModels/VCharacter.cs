using Models.Characters;
using Monitor = Models.Characters.Monitor;

namespace UI.ViewModels;

public class VCharacter
{
	private readonly Character character;

	public VCharacter(Character character)
	{
		this.character = character;
	}

	public string Id => this.character.Id;

	public Dossier Dossier => this.character.Dossier;

	public Fund Fund => this.character.Fund;

	public Gear Gear => this.character.Gear;

	public Monitor Monitor => this.character.Monitor;

	public Playbook Playbook => this.character.Playbook;

	public Rolodex Rolodex => this.character.Rolodex;

	public Talent Talent => this.character.Talent;

	public static VCharacter Empty() => new(new Character(PlaybookOption.Unknown));
}
