using Domain.Common;

namespace Domain.Characters;

public class ExperienceTracker
{
	private readonly BoundedInteger experience;

	public ExperienceTracker(int maxExperience)
	{
		this.experience = new BoundedInteger(0, maxExperience);
	}

	public int Points
	{
		get => this.experience.Value;
		set => this.experience.Value = value;
	}

	public int MaxPoints
	{
		get => this.experience.Max;
		private set => this.experience.Value = value;
	}

	public bool CanLevelUp => Points == MaxPoints;

	public void Clear()
	{
		this.experience.Value = 0;
	}
}