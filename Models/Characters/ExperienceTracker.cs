using Models.Common;

namespace Models.Characters;

public class ExperienceTracker
{
	private readonly BoundedInteger experience;

	public ExperienceTracker(int maxExperience, int points = 0)
	{
		this.experience = new BoundedInteger(0, maxExperience, points);
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