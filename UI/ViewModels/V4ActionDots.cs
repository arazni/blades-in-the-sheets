using Domain.Characters;
using Domain.Characters.Bases;

namespace UI.ViewModels;

public class V4ActionDots
{
	private readonly DossierBackground background;
	private readonly DossierHeritage heritage;
	private readonly Talent talent;

	private readonly string id;
	public string Id => this.id;

	public V4ActionDots(VCharacter character)
	{
		this.background = character.Dossier.Background;
		this.heritage = character.Dossier.Heritage;
		this.talent = character.Talent;
		this.id = character.Id;

		Attributes = new VTalentAttribute[]
		{
			new
			(
				this.talent.Insight, AttributeName.Insight, new VTalentAction[]
				{
					new(this.talent.Hunt, ActionName.Hunt),
					new(this.talent.Study, ActionName.Study),
					new(this.talent.Survey, ActionName.Survey),
					new(this.talent.Tinker, ActionName.Tinker)
				}
			),
			new
			(
				this.talent.Prowess, AttributeName.Prowess, new VTalentAction[]
				{
					new(this.talent.Finesse, ActionName.Finesse),
					new(this.talent.Prowl, ActionName.Prowl),
					new(this.talent.Skirmish, ActionName.Skirmish),
					new(this.talent.Wreck, ActionName.Wreck)
				}
			),
			new
			(
				this.talent.Resolve, AttributeName.Resolve, new VTalentAction[]
				{
					new(this.talent.Attune, ActionName.Attune),
					new(this.talent.Command, ActionName.Command),
					new(this.talent.Consort, ActionName.Consort),
					new(this.talent.Sway, ActionName.Sway)
				}
			)
		};
	}

	public const int FinalDots = 7;

	public int TotalDots =>
		Attributes.SelectMany(a => a.Actions)
			.Sum(a => a.Action.Rating);

	public int RemainingDots => FinalDots - TotalDots;

	public BackgroundOption Background => this.background.Background;

	public string BackgroundDescription => this.background.Description;

	public HeritageOption Heritage => this.heritage.Heritage;

	public string HeritageDescription => this.heritage.Description;

	public bool IsDotOneEnabled(VTalentAction action) =>
		!action.IsDotOneDefaulted && (action.IsDotOneFilled || RemainingDots > 0);

	public bool IsDotTwoEnabled(VTalentAction action) =>
		!action.IsDotTwoDefaulted && (action.IsDotTwoFilled || (action.IsDotOneFilled && RemainingDots > 0) || (RemainingDots > 1));

	public IReadOnlyCollection<VTalentAttribute> Attributes { get; }

	public static V4ActionDots Empty() => new(VCharacter.Empty());
}

public class VTalentAttribute
{
	public VTalentAttribute(TalentAttribute attribute, AttributeName name, IReadOnlyCollection<VTalentAction> actions)
	{
		Attribute = attribute;
		Name = name;
		Actions = actions;
	}

	public TalentAttribute Attribute { get; }

	public AttributeName Name { get; }

	public IReadOnlyCollection<VTalentAction> Actions { get; }
}

public class VTalentAction
{
	public VTalentAction(TalentAction action, ActionName name)
	{
		Action = action;
		Name = name;
		ShortDescription = MakeShortDescription(name);
		LongDescription = MakeLongDescription(name);
	}

	public TalentAction Action { get; }

	public bool IsDotOneDefaulted => Action.PlaybookDefault > 0;

	public bool IsDotTwoDefaulted => Action.PlaybookDefault > 1;

	public bool IsDotOneFilled => Action.Rating > 0;

	public bool IsDotTwoFilled => Action.Rating > 1;

	public void ToggleDotOne(bool toggledOn)
	{
		if (IsDotOneDefaulted)
			return;

		if (!toggledOn)
			Action.Rating = 0;
		else
			Action.Rating = 1;
	}

	public void ToggleDotTwo(bool toggledOn)
	{
		if (IsDotTwoDefaulted)
			return;

		if (!toggledOn)
			Action.Rating = 1;
		else
			Action.Rating = 2;
	}

	public ActionName Name { get; }

	public string ShortDescription { get; }

	public string LongDescription { get; }

	private static string MakeShortDescription(ActionName name) => name switch
	{
		ActionName.Attune => "When you Attune, you open your mind to arcane power.",
		ActionName.Command => "When you Command, you compel swift obedience.",
		ActionName.Consort => "When you Consort, you socialize with friends and contacts.",
		ActionName.Finesse => "When you Finesse, you employ dextrous manipulation or subtle misdirection.",
		ActionName.Hunt => "When you Hunt, you carefully track a target.",
		ActionName.Prowl => "When you Prowl, you traverse skillfully and quietly.",
		ActionName.Skirmish => "When you Skirmish, you entangle a target in close combat so they can’t easily escape.",
		ActionName.Study => "When you Study, you scrutinize details and interpret evidence.",
		ActionName.Survey => "When you Survey, you observe the situation and anticipate outcomes.",
		ActionName.Sway => "When you Sway, you influence with guile, charm, or argument.",
		ActionName.Tinker => "When you Tinker, you fiddle with devices and mechanisms.",
		ActionName.Wreck => "When you Wreck, you unleash savage force.",
		_ => throw new NotImplementedException("Unexpected enum value reached")
	};

	private static string MakeLongDescription(ActionName name) => name switch
	{
		ActionName.Attune => "You might communicate with a ghost. You could try to perceive beyond sight in order to better understand your situation (but Surveying might be better).",
		ActionName.Command => "You might intimidate or threaten to get what you want. You might lead a gang in a group action. You could try to order people around to persuade them (but Consorting might be better).",
		ActionName.Consort => "You might gain access to resources, information, people, or places. You might make a good impression or win someone over with your charm and style. You might make new friends or connect with your heritage or background. You could try to manipulate your friends with social pressure (but Sway might be better).",
		ActionName.Finesse => "You might pick someone’s pocket. You might handle the controls of a vehicle or direct a mount. You might formally duel an opponent with graceful fighting arts. You could try to employ those arts in a chaotic melee (but Skirmishing might be better). You could try to pick a lock (but Tinkering might be better).",
		ActionName.Hunt => "You might follow a target or discover their location. You might arrange an ambush. You might attack with precision shooting from a distance. You could try to bring your guns to bear in a melee (but Skirmishing might be better).",
		ActionName.Prowl => "You might sneak past a guard or hide in the shadows. You might run and leap across the rooftops. You might attack someone from hiding with a back-stab or blackjack. You could try to waylay a victim in the midst of battle (but Skirmishing might be better).",
		ActionName.Skirmish => "You might brawl or wrestle with them. You might hack and slash. You might seize or hold a position in battle. You could try to fight in a formal duel (but Finessing might be better).",
		ActionName.Study => "You might gather information from documents, newspapers, and books. You might do research on an esoteric topic. You might closely analyze a person to detect lies or true feelings. You could try to examine events to understand a pressing situation (but Surveying might be better).",
		ActionName.Survey => "You might spot telltale signs of trouble before it happens. You might uncover opportunities or weaknesses. You might detect a person’s motivations or intentions. You could try to spot a good ambush point (but Hunting might be better).",
		ActionName.Sway => "You might lie convincingly. You might persuade someone to do what you want. You might argue a compelling case that leaves no clear rebuttal. You could try to trick people into affection or obedience (but Consorting or Commanding might be better).",
		ActionName.Tinker => "You might create a new gadget or alter an existing item. You might pick a lock or crack a safe. You might disable an alarm or trap. You might turn the clockwork devices around the city to your advantage. You could try to use your technical expertise to control a vehicle (but Finessing might be better).",
		ActionName.Wreck => "You might smash down a door or wall with a sledgehammer, or use an explosive to do the same. You might employ chaos or sabotage to create a distraction or overcome an obstacle. You could try to overwhelm an enemy with sheer force in battle (but Skirmishing might be better).",
		_ => throw new NotImplementedException("Unexpected enum value reached")
	};
}