using Models.Common;
using Newtonsoft.Json;

namespace Persistence.Json.Converters;
public class BoundedCollectionStringConverter : JsonConverter<BoundedCollection<string>>
{
	public override BoundedCollection<string>? ReadJson(JsonReader reader, Type objectType, BoundedCollection<string>? existingValue, bool hasExistingValue, JsonSerializer serializer)
	{
		var startingPath = reader.Path;
		while (reader.Path == startingPath)
			reader.Read(); // open

		var capacity = existingValue?.Capacity ?? 0;

		reader.Read();

		var collection = new BoundedCollection<string>(capacity);

		for (var read = reader.ReadAsString(); read != null; read = reader.ReadAsString())
			collection.Add(read);

		while (reader.Path.EndsIn(nameof(BoundedCollectionDto.Items), "]"))
			reader.Read(); // close

		return collection;
	}

	public override void WriteJson(JsonWriter writer, BoundedCollection<string>? value, JsonSerializer serializer)
	{
		if (value == null)
			return;

		var dto = new BoundedCollectionDto(value.Capacity);

		value.CopyTo(dto.Items, 0);

		serializer.Serialize(writer, dto);
	}

	public class BoundedCollectionDto
	{
		public BoundedCollectionDto(int capacity)
		{
			Items = new string[capacity];
		}

		public string[] Items { get; set; }
	}
}
