using Models.Characters;

namespace UI.ViewModels;

public class VVice
{
	public ViceOption Option { get; set; } = ViceOption.Unknown;

	public string DisplayName => Option.ToString();

	public string Elaboration { get; set; } = string.Empty;

	public IReadOnlyCollection<string> Examples { get; set; } = Array.Empty<string>();

	public static IReadOnlyCollection<VVice> Options { get; } = new VVice[]
	{
		new VVice
		{
			Option = ViceOption.Unknown,
			Elaboration = "Choose a vice to learn more about it.",
			Examples = new string[] { "Purveyor, place, district." }
		},
		new VVice
		{
			Option = ViceOption.Faith,
			Elaboration = "You’re dedicated to an unseen power, forgotten god, ancestor, etc.",
			Examples = FaithExamples()
		},
		new VVice
		{
			Option = ViceOption.Gambling,
			Elaboration = "You crave games of chance, betting on sporting events, etc.",
			Examples = GamblingExamples()
		},
		new VVice
		{
			Option = ViceOption.Luxury,
			Elaboration = "Expensive or ostentatious displays of opulence.",
			Examples = LuxuryExamples()
		},
		new VVice
		{
			Option = ViceOption.Pleasure,
			Elaboration = "Gratification from lovers, food, drink, drugs, art, theater, etc.",
			Examples = PleasureExamples()
		},
		new VVice
		{
			Option = ViceOption.Obligation,
			Elaboration = "You're devoted to a family, a cause, an organization, a charity, etc.",
			Examples = ObligationExamples()
		},
		new VVice
		{
			Option = ViceOption.Stupor,
			Elaboration = "You seek oblivion in the abuse of drugs, drinking to excess, getting beaten to a pulp in the fighting pits, etc.",
			Examples = StuporExamples()
		},
		new VVice
		{
			Option = ViceOption.Weird,
			Elaboration = "You experiment with strange essences, consort with rogue spirits, observe bizarre rituals or taboos, etc.",
			Examples = WeirdExamples()
		}
	};

	private static IReadOnlyCollection<string> FaithExamples() => new string[]
	{
		"Mother Narya, House of the Weeping Lady, Six Towers.",
		"Ilacille, the ruins of the Temple to forgotten gods, Coalridge.",
		"Nelisanne, The Church of the Ecstasy of the Flesh, Brightstone.",
		"Lord Penderyn, the Archive of Echoes, Charterhall."
	};

	private static IReadOnlyCollection<string> GamblingExamples() => new string[]
	{
		"Spogg’s dice game, Crow’s Foot.",
		"Grist, boxing, the Docks.",
		"Helene, Silver Stag casino, Silkshore.",
		"Master Vreen, hound racing, Nightmarket.",
		"Lady Dusk, the Dusk Manor Club, Whitecrown.",
		"Sergeant Velk, the fighting pits, Dunslough."
};

	private static IReadOnlyCollection<string> LuxuryExamples() => new string[]
	{
		"Singer, bath house, Crow’s Foot.",
		"Harvale Brogan, the Centuralia Club, Brightstone.",
		"Traven’s smoke shop, Coalridge.",
		"Dunridge & Sons fine fabrics and tailoring, Nightmarket.",
		"Chef Roselle, the Golden Plum restaurant, Six Towers.",
		"Maestro Helleren, Spiregarden theater, Whitecrown."
	};

	private static IReadOnlyCollection<string> PleasureExamples() =>
		LuxuryExamples()
			.Union(StuporExamples())
			.ToArray();

	private static IReadOnlyCollection<string> ObligationExamples() => new string[]
	{
		"Family members (heritage).",
		"Former co-workers (background).",
		"Hutton, Skovlander Refugees/Revolutionaries, Charhollow.",
		"The Circle of Flame, a secret society."
	};

	private static IReadOnlyCollection<string> StuporExamples() => new string[]
	{
		"Mardin Gull, the Leaky Bucket, tavern, Crow’s Foot.",
		"Pux Bolin, the Harping Monkey, tavern, Nightmarket.",
		"Helene, Silver Stag casino, Silkshore.",
		"Lady Freyla, the Emperor’s Cask, bar, Whitecrown.",
		"Avrick, powder dealer, Barrowcleft.",
		"Rolan Volaris, the Veil, social club, Nightmarket.",
		"Madame Tesslyn, the Red Lamp, brothel, Silkshore.",
		"Traven’s smoke shop, Coalridge.",
		"Eldrin Prichard, the Silver Swan pleasure barge, Brightstone canals.",
		"Jewel, Bird, and Shine, Catcrawl Alley, the Docks."
	};

	private static IReadOnlyCollection<string> WeirdExamples() => new string[]
	{
		"The hooded proprietor of a half-flooded grotto tavern near the docks. ",
		"Strange passageways lead to stranger chambers beyond.",
		"Father Yoren, House of the Weeping Lady, Six Towers.",
		"“Salia,” a spirit of the Reconciled, which moves from body to body at their whim.",
		"Sister Thorn, deathlands scavenger gang, Gaddoc Station.",
		"Ojak, Tycherosi rooftop market vendor, Silkshore.",
		"Aranna the Blessed, cultist of a forgotten god, barge moored in Nightmarket."
	};
}
