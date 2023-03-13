using Models.Characters;
using Newtonsoft.Json;

namespace Persistence.Json.Converters;
public class ExperienceTrackerConverter : JsonConverter<ExperienceTracker>
{
	public override ExperienceTracker? ReadJson(JsonReader reader, Type objectType, ExperienceTracker? existingValue, bool hasExistingValue, JsonSerializer serializer)
	{
		reader.Read(); // open

		var points = reader.ReadAsInt32() ?? 0;
		reader.Read();

		int max;
		if (existingValue == null)
			max = reader.ReadAsInt32() ?? 0;
		else
		{
			max = existingValue.MaxPoints;
			reader.Read();
		}

		reader.Read(); // close

		return new ExperienceTracker(max, points);
	}

	public override void WriteJson(JsonWriter writer, ExperienceTracker? value, JsonSerializer serializer)
	{
		if (value == null)
			return;

		serializer.Serialize(writer, new ExperienceDto
		{
			Points = value.Points,
			MaxPoints = value.MaxPoints,
		});
	}

	public class ExperienceDto
	{
		public int Points { get; set; }
		public int MaxPoints { get; set; }
	}
}
