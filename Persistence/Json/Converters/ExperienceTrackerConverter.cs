using Models.Characters;
using Newtonsoft.Json;

namespace Persistence.Json.Converters;
public class ExperienceTrackerConverter : JsonConverter<ExperienceTracker>
{
	public override ExperienceTracker? ReadJson(JsonReader reader, Type objectType, ExperienceTracker? existingValue, bool hasExistingValue, JsonSerializer serializer)
	{
		reader.Read();
		var max = reader.ReadAsInt32() ?? 0;
		reader.Read();
		var points = reader.ReadAsInt32() ?? 0;
		reader.Read();

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
		public int MaxPoints { get; set; }
		public int Points { get; set; }
	}
}
