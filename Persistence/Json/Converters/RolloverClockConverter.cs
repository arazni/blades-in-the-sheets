using Models.Game;
using Newtonsoft.Json;

namespace Persistence.Json.Converters;
public class RolloverClockConverter : JsonConverter<RolloverClock>
{
	public override RolloverClock? ReadJson(JsonReader reader, Type objectType, RolloverClock? existingValue, bool hasExistingValue, JsonSerializer serializer)
	{
		reader.Read(); // open

		var valuesByProperties = new Dictionary<string, int>();
		var defaultsByProperties = new Dictionary<string, int>
		{
			{ nameof(RolloverClockDto.Size), 4 },
			{ nameof(RolloverClockDto.Time), 0 },
			{ nameof(RolloverClockDto.Rollover), 0 }
		};

		FillDictionary(reader, valuesByProperties, defaultsByProperties);
		FillDictionary(reader, valuesByProperties, defaultsByProperties);
		FillDictionary(reader, valuesByProperties, defaultsByProperties);

		return new RolloverClock
		(
			valuesByProperties[nameof(RolloverClockDto.Size)],
			valuesByProperties[nameof(RolloverClockDto.Time)],
			valuesByProperties[nameof(RolloverClockDto.Rollover)]
		);
	}

	private static void FillDictionary(JsonReader reader, Dictionary<string, int> valuesByProperties, Dictionary<string, int> defaultsByProperties)
	{
		var propertyName = reader.Path.Split(".").Last();
		valuesByProperties.Add(propertyName, reader.ReadAsInt32() ?? defaultsByProperties[propertyName]);
		reader.Read();
	}

	public override void WriteJson(JsonWriter writer, RolloverClock? value, JsonSerializer serializer)
	{
		if (value == null)
			return;

		serializer.Serialize(writer, new RolloverClockDto
		{
			Time = value.Time,
			Rollover = value.Rollover,
			Size = value.Size
		});
	}

	public class RolloverClockDto
	{
		public int Time { get; set; }

		public int Rollover { get; set; }

		public int Size { get; set; }
	}
}
