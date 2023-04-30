using Models.Interactions;
using Models.Settings;
using Newtonsoft.Json;

namespace Models.Characters;

public class TalentAttribute : IRollable
{
	private TalentAttribute() { }

	public TalentAttribute(AttributeSetting setting, int actionPointsMaximum)
	{
		ActionsByName = setting.Actions.ToDictionary
		(
			setting => setting.Name,
			_ => new TalentAction(actionPointsMaximum),
			StringComparer.OrdinalIgnoreCase
		);
	}

	[JsonProperty]
	public IReadOnlyDictionary<string, TalentAction> ActionsByName { get; private set; } = new Dictionary<string, TalentAction>();

	public ExperienceTracker Experience { get; private set; } = new(6);

	public int Rating =>
		ActionsByName.Values.Count(a => a.Rating > 0);

	public bool LevelUp(string name)
	{
		if (!Experience.CanLevelUp)
			return false;

		if (!ActionsByName.TryGetValue(name, out var action))
			return false;

		if (action.HasMaxRating)
			return false;

		action.Rating++;
		Experience.Clear();

		return true;
	}

	public static TalentAttribute Empty() => new(EmptyGameSetting.Attribute(), 4);
}
