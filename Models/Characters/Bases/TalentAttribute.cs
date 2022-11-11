using Models.Interactions;
using System.Reflection;

namespace Models.Characters.Bases;

public abstract class TalentAttribute : IRollable
{
	public ExperienceTracker Experience { get; } = new(6);

	public int Rating =>
		GetType()
			.GetProperties()
			.Where(p => p.PropertyType.IsAssignableTo(typeof(TalentAction)))
			.Select(p => p.GetValue(this) as TalentAction ?? throw new InvalidCastException())
			.Count(a => a.Rating > 0);

	public IReadOnlyCollection<PropertyInfo> GetActions() =>
		GetType()
			.GetProperties()
			.Where(p => p.PropertyType.IsAssignableTo(typeof(TalentAction)))
			.ToArray();

	public abstract TalentAction ActionFromName(ActionName name);

	public bool LevelUp(ActionName name)
	{
		if (!Experience.CanLevelUp)
			return false;

		var action = ActionFromName(name);

		if (action.HasMaxRating)
			return false;

		action.Rating++;
		Experience.Clear();

		return true;
	}

	public static ActionName[] GetAttributeNames(AttributeName name) =>
		Enumerable.Range(0, 4)
			.Select(i => (ActionName)(4 * (int)name + i))
			.ToArray();
}
