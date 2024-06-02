namespace Models.Settings;

public static class EmptyGameSetting
{
	private static readonly ActionSetting action = new(string.Empty, string.Empty, string.Empty);
	private static readonly AttributeSetting attribute = new(string.Empty, []);
	private static readonly BackgroundSetting background = new(string.Empty, string.Empty, string.Empty);
	private static readonly DefaultActionPointSetting defaultActionPoint = new(string.Empty, 0);
	private static readonly HeritageSetting heritage = new(string.Empty, string.Empty, string.Empty);
	private static readonly RolodexSetting rolodex = new(string.Empty, []);
	private static readonly SpecialAbilitySetting specialAbility = new(string.Empty, 1, string.Empty);
	private static readonly PlaybookSetting playbook = new
	(
		string.Empty,
		string.Empty,
		[],
		Rolodex(),
		[],
		[],
		string.Empty
	);
	private static readonly ThesaurusSetting thesaurus = new();
	private static readonly ExtraDescriptionSetting extraDescription = new([]);

	public static ActionSetting Action() => action;

	public static AttributeSetting Attribute() => attribute;

	public static AttributeSetting[] Attributes() => [];

	public static BackgroundSetting Background() => background;

	public static BackgroundSetting[] Backgrounds() => [];

	public static DefaultActionPointSetting DefaultActionPoint() => defaultActionPoint;

	public static HeritageSetting Heritage() => heritage;

	public static HeritageSetting[] Heritages() => [];

	public static RolodexSetting Rolodex() => rolodex;

	public static ViceSetting[] Vices() => [];

	public static string[] Traumas() => [];

	public static GearItemSetting[] GearItems() => [];

	public static PlaybookSetting Playbook() => playbook;

	public static PlaybookSetting[] Playbooks() => [];

	public static SpecialAbilitySetting SpecialAbility => specialAbility;

	public static SpecialAbilitySetting[] PlaybookSpecialAbilities() => [];

	public static ThesaurusSetting Thesaurus() => thesaurus;

	public static ExtraDescriptionSetting ExtraDescription() => extraDescription;

	public static GameSetting Game() => new
	(
		string.Empty,
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
		4,
		null,
		null
	);
}
