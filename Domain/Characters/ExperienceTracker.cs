using Domain.Common;

namespace Domain.Characters;

public class ExperienceTracker
{
	private readonly BoundedInteger experience;

	internal ExperienceTracker(int maxExperience)
	{
		this.experience = new BoundedInteger(0, maxExperience);
	}

	public int Experience
	{
		get => this.experience.Value;
		set => this.experience.Value = value;
	}

	internal int MaxExperience => this.experience.Max;

	internal bool CanLevelUp => Experience == MaxExperience;

	internal void Clear()
	{
		this.experience.Value = 0;
	}
}