using Models.Characters;

namespace Models.Settings;

public static class EmptyGameSetting
{
	private static readonly ActionSetting action = new(string.Empty, string.Empty, string.Empty);
	private static readonly AttributeSetting attribute = new(string.Empty, Array.Empty<ActionSetting>());
	private static readonly BackgroundSetting background = new(string.Empty, string.Empty, string.Empty);
	private static readonly DefaultActionPointSetting defaultActionPoint = new(string.Empty, 0);
	private static readonly HeritageSetting heritage = new(string.Empty, string.Empty, string.Empty);
	private static readonly RolodexSetting rolodex = new(string.Empty, Array.Empty<string>());
	private static readonly PlaybookSetting playbook = new
	(
		string.Empty,
		string.Empty,
		Array.Empty<SpecialAbilitySetting>(),
		RolodexSetting(),
		Array.Empty<GearItemSetting>(),
		Array.Empty<DefaultActionPointSetting>(),
		string.Empty
	);
	private static readonly ThesaurusSetting thesaurus = new();

	public static ActionSetting Action() => action;

	public static AttributeSetting Attribute() => attribute;

	public static AttributeSetting[] Attributes() => Array.Empty<AttributeSetting>();

	public static BackgroundSetting Background() => background;

	public static BackgroundSetting[] Backgrounds() => Array.Empty<BackgroundSetting>();

	public static DefaultActionPointSetting DefaultActionPoint() => defaultActionPoint;

	public static HeritageSetting Heritage() => heritage;

	public static HeritageSetting[] Heritages() => Array.Empty<HeritageSetting>();

	public static RolodexSetting RolodexSetting() => rolodex;

	public static ViceSetting[] Vices() => Array.Empty<ViceSetting>();

	public static string[] Traumas() => Array.Empty<string>();

	public static GearItemSetting[] GearItems() => Array.Empty<GearItemSetting>();

	public static PlaybookSetting Playbook() => playbook;

	public static PlaybookSetting[] Playbooks() => Array.Empty<PlaybookSetting>();

	public static PlaybookSpecialAbility[] PlaybookSpecialAbilities() => Array.Empty<PlaybookSpecialAbility>();

	public static ThesaurusSetting Thesaurus() => thesaurus;

	public static GameSetting Game() => new
	(
		string.Empty,
		Playbooks(),
		Traumas(),
		Heritages(),
		Vices(),
		Backgrounds(),
		Attributes(),
		GearItems(),
		Thesaurus(),
		4,
		4
	);
}
