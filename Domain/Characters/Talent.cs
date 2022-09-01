using System.Reflection;

namespace Domain.Characters;

public class Talent
{
	public Talent(PlaybookOption playbook)
	{
		AssignDefaultRatings(playbook);
	}

	public TalentInsight Insight { get; private set; } = new();

	public TalentProwess Prowess { get; private set; } = new();

	public TalentResolve Resolve { get; private set; } = new();

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

	public IReadOnlyCollection<PropertyInfo> GetAttributes() =>
		GetType()
			.GetProperties()
			.Where(p => p.PropertyType.IsAssignableTo(typeof(Bases.TalentAttribute)))
			.ToArray();

	private void AssignDefaultRatings(PlaybookOption playbook)
	{
		switch (playbook)
		{
			case PlaybookOption.Cutter:
				Skirmish.PlaybookDefault = 2;
				Command.PlaybookDefault = 1;
				break;
			case PlaybookOption.Hound:
				Hunt.PlaybookDefault = 2;
				Survey.PlaybookDefault = 1;
				break;
			case PlaybookOption.Leech:
				Tinker.PlaybookDefault = 2;
				Wreck.PlaybookDefault = 1;
				break;
			case PlaybookOption.Lurk:
				Prowl.PlaybookDefault = 2;
				Finesse.PlaybookDefault = 1;
				break;
			case PlaybookOption.Slide:
				Sway.PlaybookDefault = 2;
				Consort.PlaybookDefault = 1;
				break;
			case PlaybookOption.Spider:
				Consort.PlaybookDefault = 2;
				Study.PlaybookDefault = 1;
				break;
			case PlaybookOption.Whisper:
				Attune.PlaybookDefault = 2;
				Study.PlaybookDefault = 1;
				break;
		}
	}

	public static Talent Empty() => new(PlaybookOption.Unknown);
}

public enum AttributeName
{
	Insight,
	Prowess,
	Resolve
}