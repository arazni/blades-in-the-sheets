using Domain.Common;
using Domain.Interactions;

namespace Domain.Characters;

public class TalentAction : IRollable
{
	private readonly BoundedInteger rating = new(0, 4);

	//public TalentAction(Actions action)
	//{
	//	Type = action;
	//}

	//public Actions Type { get; }

	public int Rating
	{
		get => this.rating.Value;
		set => this.rating.Value = value;
	}
}

public enum Actions
{
	// Insight
	Hunt,
	Study,
	Survey,
	Tinker,
	// Prowess
	Finesse,
	Prowl,
	Skirmish,
	Wreck,
	// Resolve
	Attune,
	Command,
	Consort,
	Sway
}