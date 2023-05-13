using Models.Characters;
using Models.Settings;
using static UI.Constants;

namespace UI.ViewModels;

public class VIndexCharacter
{
	public VIndexCharacter(Character character)
	{
		Id = character.Id;
		Name = character.Dossier.Name;
		Alias = character.Dossier.Alias;
		Playbook = character.Playbook.Name;
		GameName = character.GameName;
		Background = character.Dossier.Background.Name;
		Heritage = character.Dossier.Heritage.Name;
	}

	public string Id { get; }

	public string Name { get; }

	public string Alias { get; }

	public string Playbook { get; }

	public string GameName { get; }

	public string Background { get; }

	public string Heritage { get; }

	public string Blurb(GameSetting gameSetting) => $"{Name}, {gameSetting.GetBackground(Background).BlurbFlavor} from {gameSetting.GetHeritage(Heritage).BlurbFlavor}";

	public string Link => Paths.Sheet(Id);
}
