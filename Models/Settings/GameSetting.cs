namespace Models.Settings;

public record GameSetting
(
	string Name,
	PlaybookSetting[] Playbooks,
	string[] Traumas,
	HeritageSetting[] Heritages,
	ViceSetting[] Vices,
	BackgroundSetting[] Backgrounds,
	AttributeSetting[] Attributes,
	GearItemSetting[] SharedItems,
	ThesaurusSetting Thesaurus,
	int RecoveryClockSize,
	int ActionPointMaximum,
	StartingAbilitySetting? StartingAbility = null
);

public record PlaybookSetting
(
	string Name,
	string Hook,
	SpecialAbilitySetting[] SpecialAbilities,
	RolodexSetting Rolodex,
	GearItemSetting[] Items,
	DefaultActionPointSetting[] DefaultActionPoints,
	string ExperienceCondition,
	string AliasSynonym = Constants.DefaultNames.Alias
);

public record SpecialAbilitySetting
(
	string Name,
	int TimesTakeable,
	string Description
);

public record RolodexSetting
(
	string Name,
	string[] Friends
);

public record GearItemSetting
(
	string Name,
	int Bulk
);

public record DefaultActionPointSetting
(
	string Action,
	int Points
);

public record HeritageSetting
(
	string Name,
	string Description,
	string BlurbFlavor
);

public record ViceSetting
(
	string Name,
	string Description,
	string[] Sources
);

public record BackgroundSetting
(
	string Name,
	string Example,
	string BlurbFlavor
);

public record AttributeSetting
(
	string Name,
	ActionSetting[] Actions
);

public record ActionSetting
(
	string Name,
	string ShortDescription,
	string LongDescription
);

public record ThesaurusSetting
(
	string Coin = Constants.DefaultNames.Coin,
	string Bulk = Constants.DefaultNames.Bulk
);

public record StartingAbilitySetting
(
	Dictionary<string, StartingSpecialAbilitySetting> AbilitiesByPlaybook,
	Dictionary<string, StartingSpecialAbilitySetting> AbilitiesByHeritage
);

public record StartingSpecialAbilitySetting
(
	string Name,
	string Description
);
