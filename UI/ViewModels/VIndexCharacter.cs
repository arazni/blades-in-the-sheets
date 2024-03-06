using Models.Characters;
using Models.Settings;
using static UI.Constants;

namespace UI.ViewModels;

public class VIndexCharacter(Character character)
{
	public string Id { get; } = character.Id;

	public string Name { get; } = character.Dossier.Name;

	public string Alias { get; } = character.Dossier.Alias;

	public string Playbook { get; } = character.Playbook.Name;

	public string GameName { get; } = character.GameName;

	public string Language { get; } = character.Language;

	public string Background { get; } = character.Dossier.Background.Name;

	public string Heritage { get; } = character.Dossier.Heritage.Name;

	public string Blurb(GameSetting gameSetting) => $"{Name}, {gameSetting.GetBackground(Background).BlurbFlavor} {gameSetting.GetHeritage(Heritage).BlurbFlavor}";

	public string Link => Paths.Sheet(Id);
}
