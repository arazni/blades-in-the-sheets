using Domain.Common;
using Domain.Interactions;

namespace Domain.Characters;

public class TalentAction : IRollable
{
	private readonly BoundedInteger rating = new(0, 4);
	private int playbookDefault;

	public int Rating
	{
		get => this.rating.Value;
		set => this.rating.Value = value;
	}

	public int MaxRating =>
		this.rating.Max;

	public bool HasMaxRating =>
		MaxRating == this.rating.Value;

	public int PlaybookDefault
	{
		get => this.playbookDefault;
		internal set
		{
			if (value > 2 || value < 0)
				throw new ArgumentException("PlaybookDefault out of bounds", nameof(value));

			this.playbookDefault = value;
			Rating = Math.Max(Rating, value);
		}
	}

	public static AttributeName GetAttributeName(ActionName name) =>
		(AttributeName)((int)name / 4);
}

public enum ActionName
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