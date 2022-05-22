using Domain.Interactions;

namespace Domain.Characters.Bases;

public abstract class TalentAttribute : IRollable
{
	public int Rating =>
		GetType()
			.GetProperties()
			.Where(p => p.PropertyType.IsAssignableTo(typeof(TalentAction)))
			.Select(p => p.GetValue(this) as TalentAction ?? throw new InvalidCastException())
			.Count(a => a.Rating > 0);
}
