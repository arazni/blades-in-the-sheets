using Models.Game;
using Newtonsoft.Json;

namespace Persistence.Json.Converters;
public class RolloverClockConverter : JsonConverter<RolloverClock>
{
	public override RolloverClock? ReadJson(JsonReader reader, Type objectType, RolloverClock? existingValue, bool hasExistingValue, JsonSerializer serializer)
	{
		reader.Read(); // open

		var max = existingValue?.Size ?? 0;

		var time = reader.ReadAsInt32() ?? 0;
		reader.Read();

		var rollover = reader.ReadAsInt32() ?? 0;

		reader.Read(); // close

		return new RolloverClock(max, time, rollover);
	}

	public override void WriteJson(JsonWriter writer, RolloverClock? value, JsonSerializer serializer)
	{
		if (value == null)
			return;

		serializer.Serialize(writer, new RolloverClockDto
		{
			Time = value.Time,
			Rollover = value.Rollover
		});
	}

	public class RolloverClockDto
	{
		public int Time { get; set; }

		public int Rollover { get; set; }
	}
}
