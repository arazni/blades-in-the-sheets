using Models.Characters;
using static UI.Constants;

namespace UI.ViewModels;

public class VIndexCharacter
{
	private readonly string backgroundBlurb;
	private readonly string heritageBlurb;

	public VIndexCharacter(Character character, string backgroundBlurb, string heritageBlurb)
	{
		Id = character.Id;
		Name = character.Dossier.Name;
		Alias = character.Dossier.Alias;
		Playbook = character.Playbook.Name;
		this.backgroundBlurb = backgroundBlurb;
		this.heritageBlurb = heritageBlurb;
	}

	public string Id { get; }

	public string Name { get; }

	public string Alias { get; }

	public string Playbook { get; }

	public string Blurb => $"{Name}, {this.backgroundBlurb} from {this.heritageBlurb}";

	public string Link => Paths.Sheet(Id);

}
