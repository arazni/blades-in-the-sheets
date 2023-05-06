﻿namespace Models.Settings;

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
	string? AliasSynonym = "Alias"
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
	string Coin = "Coin",
	string Bulk = "Bulk"
);

public record StartingAbilitySetting
(
	PlaybookStartingAbilitySetting[] PlaybookStartingAbilities,
	HeritageStartingAbilitySetting[] HeritageStartingAbilities
);

public record PlaybookStartingAbilitySetting
(
	string Playbook,
	string Name,
	string Description
);

public record HeritageStartingAbilitySetting
(
	string Heritage,
	string Name,
	string Description
);
