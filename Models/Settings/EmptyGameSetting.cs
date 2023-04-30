namespace Models.Settings;

public static class EmptyGameSetting
{
	public static AttributeSetting Attribute() =>
		new(string.Empty, Array.Empty<ActionSetting>());

	public static BackgroundSetting[] Backgrounds() =>
		Array.Empty<BackgroundSetting>();

	public static HeritageSetting[] Heritages() =>
		Array.Empty<HeritageSetting>();

	public static RolodexSetting RolodexSetting() =>
		new(string.Empty, Array.Empty<string>());

	public static ViceSetting[] Vices() =>
		Array.Empty<ViceSetting>();
}
