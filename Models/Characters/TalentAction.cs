using Models.Common;
using Models.Interactions;

namespace Models.Characters;

public class TalentAction : IRollable
{
	private readonly BoundedInteger rating;

	public TalentAction(int maxRating)
	{
		this.rating = new BoundedInteger(maxRating);
	}

	public int MaxRating
	{
		get => this.rating.Max;
		private set => this.rating.Max = value;
	}

	public int Rating
	{
		get => this.rating.Value;
		set => this.rating.Value = value;
	}

	public bool HasMaxRating =>
		MaxRating == this.rating.Value;

	public static TalentAction Empty() => new(4);
}
