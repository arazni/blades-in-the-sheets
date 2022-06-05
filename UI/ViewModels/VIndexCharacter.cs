using Domain.Characters;
using static UI.Constants;

namespace UI.ViewModels;

public class VIndexCharacter
{
	private readonly BackgroundOption background;
	private readonly PlaybookOption playbook;
	private readonly HeritageOption heritage;

	public VIndexCharacter(Character character)
	{
		Id = character.Id;
		Name = character.Dossier.Name;
		Alias = character.Dossier.Alias;
		this.playbook = character.Playbook.Option;
		this.background = character.Dossier.Background.Background;
		this.heritage = character.Dossier.Heritage.Heritage;
	}

	public string Id { get; }

	public string Name { get; }

	public string Alias { get; }

	public string Playbook => this.playbook.ToString();

	public string Blurb => $"{Name}, {BlurbBackground} from {BlurbHeritage}";

	public string Link => Paths.Character(Id);

	private string BlurbBackground => this.background switch
	{
		BackgroundOption.Unknown => "a mysterious",
		BackgroundOption.Academic => "an academic",
		BackgroundOption.Labor => "a laborer",
		BackgroundOption.Law => "an \"incorruptible\" representative of the law",
		BackgroundOption.Trade => "a trader",
		BackgroundOption.Military => "an enlisted",
		BackgroundOption.Noble => "a noble",
		BackgroundOption.Underworld => "a \"law-abiding citizen\"",
		_ => throw new NotImplementedException("Unexpected enum value reached")
	};

	private string BlurbHeritage => this.heritage switch
	{
		HeritageOption.Unknown => "a mysterious place",
		HeritageOption.Akoros => "'round here in Akoros",
		HeritageOption.TheDaggerIsles => "the Dagger Isles",
		HeritageOption.Iruvia => "the desert kingdom of Iruvia",
		HeritageOption.Severos => "the deathlands of Severos",
		HeritageOption.Skovlan => "the colonized island of Skovlan",
		HeritageOption.Tycheros => "the weird, distant land of Tycheros",
		_ => throw new NotImplementedException("Unexpected enum value reached")
	};
}
