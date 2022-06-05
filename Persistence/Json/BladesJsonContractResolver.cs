using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Persistence.Json;

public class BladesJsonContractResolver : DefaultContractResolver
{
	protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
	{
		var property = base.CreateProperty(member, memberSerialization);

		property.ShouldSerialize = _ => ShouldSerialize(member);

		WriteToPrivateSetter(property, member);

		return property;
	}

	internal static bool ShouldSerialize(MemberInfo memberInfo)
	{
		var propertyInfo = memberInfo as PropertyInfo;
		if (propertyInfo == null)
			return false;

		if (propertyInfo.SetMethod != null)
			return true;

		var getMethod = propertyInfo.GetMethod;
		if (getMethod == null)
			throw new InvalidOperationException("This property somehow has neither a getter nor a setter");

		var should = Attribute.GetCustomAttribute(getMethod, typeof(CompilerGeneratedAttribute)) != null;

		return should;
	}

	protected static void WriteToPrivateSetter(JsonProperty prop, MemberInfo member)
	{
		if (prop.Writable)
			return;

		var property = member as PropertyInfo;
		var hasPrivateSetter = property?.GetSetMethod(true) != null;
		prop.Writable = hasPrivateSetter;
	}
}