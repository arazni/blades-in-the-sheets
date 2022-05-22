//using System.Collections.Immutable;

//namespace Domain.Characters;

//public class TalentAttribute
//{
//	public TalentAttribute(Attributes attribute)
//	{
//		Type = attribute;
//		ActionDictionary = BuildActions(attribute);
//	}

//	public Attributes Type { get; }

//	internal IReadOnlyDictionary<Actions, TalentAction> ActionDictionary { get; }

//	public int Resistance =>
//		ActionDictionary.Select(a => a.Value)
//			.Where(ta => ta.Rating > 0)
//			.Count();

//	public ExperienceTracker Experience { get; } = new ExperienceTracker(6);

//	private static IReadOnlyDictionary<Actions, TalentAction> BuildActions(Attributes attribute) => attribute switch
//	{
//		Attributes.Insight => new TalentAction[]
//		{
//			new TalentAction(Actions.Hunt),
//			new TalentAction(Actions.Study),
//			new TalentAction(Actions.Survey),
//			new TalentAction(Actions.Tinker)
//		}
//		.ToImmutableDictionary(ta => ta.Type),
//		Attributes.Prowess => new TalentAction[]
//		{
//			new TalentAction(Actions.Finesse),
//			new TalentAction(Actions.Prowl),
//			new TalentAction(Actions.Skirmish),
//			new TalentAction(Actions.Wreck)
//		}
//		.ToImmutableDictionary(ta => ta.Type),
//		Attributes.Resolve => new TalentAction[]
//		{
//			new TalentAction(Actions.Attune),
//			new TalentAction(Actions.Command),
//			new TalentAction(Actions.Consort),
//			new TalentAction(Actions.Sway)
//		}
//		.ToImmutableDictionary(ta => ta.Type),
//		_ => throw new InvalidOperationException("Impossible attribute")
//	};
//}

public enum Attributes
{
	Insight,
	Prowess,
	Resolve
}
