namespace Domain.Characters;

public class Talent
{
	public TalentInsight Insight { get; } = new();

	public TalentProwess Prowess { get; } = new();

	public TalentResolve Resolve { get; } = new();

	public TalentAction Hunt => Insight.Hunt;

	public TalentAction Study => Insight.Study;

	public TalentAction Survey => Insight.Survey;

	public TalentAction Tinker => Insight.Tinker;

	public TalentAction Finesse => Prowess.Finesse;

	public TalentAction Prowl => Prowess.Prowl;

	public TalentAction Skirmish => Prowess.Skirmish;

	public TalentAction Wreck => Prowess.Wreck;

	public TalentAction Attune => Resolve.Attune;

	public TalentAction Command => Resolve.Command;

	public TalentAction Consort => Resolve.Consort;

	public TalentAction Sway => Resolve.Sway;
}

/*
public class Talent
{
	public TalentAttribute Insight { get; } = new(Attributes.Insight);

	public TalentAttribute Prowess { get; } = new(Attributes.Prowess);

	public TalentAttribute Resolve { get; } = new(Attributes.Resolve);

	public TalentAction Hunt => Insight.ActionDictionary[Actions.Hunt];

	public TalentAction Study => Insight.ActionDictionary[Actions.Study];

	public TalentAction Survey => Insight.ActionDictionary[Actions.Survey];

	public TalentAction Tinker => Insight.ActionDictionary[Actions.Tinker];

	public TalentAction Finesse => Prowess.ActionDictionary[Actions.Finesse];

	public TalentAction Prowl => Prowess.ActionDictionary[Actions.Prowl];

	public TalentAction Skirmish => Prowess.ActionDictionary[Actions.Skirmish];

	public TalentAction Wreck => Prowess.ActionDictionary[Actions.Wreck];

	public TalentAction Attune => Resolve.ActionDictionary[Actions.Attune];

	public TalentAction Command => Resolve.ActionDictionary[Actions.Command];

	public TalentAction Consort => Resolve.ActionDictionary[Actions.Consort];

	public TalentAction Sway => Resolve.ActionDictionary[Actions.Sway];
}
*/
