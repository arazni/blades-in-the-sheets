using Models.Characters;
using Models.Settings;
using static UI.Constants;

namespace UI.ViewModels;

public class VIndexCharacter
{
	private readonly string backgroundBlurb;
	private readonly string heritageBlurb;

	public VIndexCharacter(GameSetting gameSetting, Character character)
	{
		Id = character.Id;
		Name = character.Dossier.Name;
		Alias = character.Dossier.Alias;
		Playbook = character.Playbook.Name;
		GameName = character.GameName;
		this.backgroundBlurb = gameSetting.GetBackground(character.Dossier.Background.Name).BlurbFlavor;
		this.heritageBlurb = gameSetting.GetHeritage(character.Dossier.Heritage.Name).BlurbFlavor;
	}

	public string Id { get; }

	public string Name { get; }

	public string Alias { get; }

	public string Playbook { get; }

	public string GameName { get; }

	public string Blurb => $"{Name}, {this.backgroundBlurb} from {this.heritageBlurb}";

	public string Link => Paths.Sheet(Id);
}
