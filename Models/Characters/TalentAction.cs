using Models.Common;
using Models.Interactions;

namespace Models.Characters;

public class TalentAction : IRollable
{
	private TalentAction() { }

	private readonly BoundedInteger rating = new(4);

	public TalentAction(int actionPointsMaximum)
	{
		this.rating = new BoundedInteger(actionPointsMaximum);
	}

	public int Rating
	{
		get => this.rating.Value;
		set => this.rating.Value = value;
	}

	public int MaxRating =>
		this.rating.Max;

	public bool HasMaxRating =>
		MaxRating == this.rating.Value;

	public static TalentAction Empty() => new();
}
