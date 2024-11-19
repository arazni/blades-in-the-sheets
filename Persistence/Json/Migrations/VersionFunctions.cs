using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Persistence.Json.Migrations;

internal static class VersionFunctions
{
	internal static void NestProperty(JObject json, string path, string childPropertyName, params string[] excludingPropertyNames)
	{
		var parentProperty = json.SelectToken(path) as JObject
			?? throw Error(path);

		var children = parentProperty.DeepClone();

		foreach (var child in children.Children().IntersectBy(excludingPropertyNames, token => (token as JProperty)?.Name).ToArray())
			child.Remove();

		foreach (var child in parentProperty.Children().ExceptBy(excludingPropertyNames, token => (token as JProperty)?.Name).ToArray())
			child.Remove();

		parentProperty.Add(childPropertyName, children);
	}

	internal static void AddObject<T>(JObject json, string path, string objectName, T obj) where T : notnull
	{
		var parentProperty = json.SelectToken(path) as JObject
			?? throw Error(path);

		var addable = JObject.FromObject(obj);
		parentProperty.Add(objectName, addable);
	}

	internal static void AddProperty<T>(JObject json, string path, string propertyName, T propertyValue)
	{
		var parentProperty = json.SelectToken(path) as JObject
			?? throw Error(path);

		parentProperty.Add(propertyName, new JValue(propertyValue));
	}

	internal static void ReplaceVersion(JObject json, int version)
	{
		ReplacePropertyValue(json, "$", "Version", version);
	}

	internal static void ReplacePropertyValue<T>(JObject json, string path, string propertyName, T propertyValue)
	{
		var pathIncludingProperty = $"{path}.{propertyName}";

		var toReplace = json.SelectToken(pathIncludingProperty)?.Parent as JProperty
			?? throw Error(pathIncludingProperty);

		toReplace.Replace(new JProperty(propertyName, propertyValue));
	}

	internal static JsonException Error(string jPath) =>
		new($"Cannot migrate to v2. Cannot find JPATH {jPath}");
}
