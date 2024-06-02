using Models.Characters;
using Models.Common;

namespace Models.Settings;

public static class GameSettingExtensions
{
	public static PlaybookSetting GetPlaybookSetting(this GameSetting gameSetting, string playbookName) =>
		gameSetting == EmptyGameSetting.Game() ? EmptyGameSetting.Playbook()
			: gameSetting.Playbooks.FirstOrDefault(p => p.Name == playbookName)
				?? throw new ArgumentOutOfRangeException(nameof(playbookName), $"{playbookName} is not one of the recognized playbooks in the settings for {gameSetting.Name}: {gameSetting.Playbooks.Join(", ")}");

	public static AttributeSetting GetAttributeSetting(this GameSetting gameSetting, string attributeName) =>
		gameSetting == EmptyGameSetting.Game() ? EmptyGameSetting.Attribute()
		: gameSetting.Attributes.FirstOrDefault(a => a.Name == attributeName)
			?? throw new ArgumentOutOfRangeException(nameof(attributeName), $"{attributeName} is not one of the recognized attributes in the settings for {gameSetting.Name}: {gameSetting.Attributes.Join(", ")}");

	public static ActionSetting GetActionSetting(this GameSetting gameSetting, string attributeName, string actionName) =>
		gameSetting == EmptyGameSetting.Game() ? EmptyGameSetting.Action()
			: gameSetting.GetAttributeSetting(attributeName)
				.Actions.FirstOrDefault(a => a.Name == actionName)
			?? throw new ArgumentOutOfRangeException(nameof(actionName), $"{actionName} is not one of the recognized actions in the settings for {gameSetting.Name}: {gameSetting.GetAttributeSetting(attributeName).Actions.Select(a => a.Name).Join(", ")}");

	public static SpecialAbilitySetting[] GetAvailableAbilities(this PlaybookSetting playbookSetting) =>
		playbookSetting == EmptyGameSetting.Playbook() ? EmptyGameSetting.PlaybookSpecialAbilities()
		: playbookSetting.SpecialAbilities;

	public static HeritageSetting GetHeritage(this GameSetting gameSetting, string heritageName) =>
		gameSetting == EmptyGameSetting.Game() ? EmptyGameSetting.Heritage()
		: gameSetting.Heritages.FirstOrDefault(h => h.Name == heritageName)
			?? throw new ArgumentOutOfRangeException(nameof(heritageName), $"{heritageName} is not one of the recognized heritages in the settings for {gameSetting.Name}: {gameSetting.Heritages.Join(", ")}");

	public static BackgroundSetting GetBackground(this GameSetting gameSetting, string backgroundName) =>
		gameSetting == EmptyGameSetting.Game() ? EmptyGameSetting.Background()
		: gameSetting.Backgrounds.FirstOrDefault(h => h.Name == backgroundName)
			?? throw new ArgumentOutOfRangeException(nameof(backgroundName), $"{backgroundName} is not one of the recognized heritages in the settings for {gameSetting.Name}: {gameSetting.Backgrounds.Join(", ")}");

	public static int DefaultActionPoints(this GameSetting gameSetting, string playbookName, string actionName) =>
		gameSetting.GetPlaybookSetting(playbookName)
			.DefaultActionPoints(actionName);

	public static int DefaultActionPoints(this PlaybookSetting playbookSetting, string actionName) =>
		playbookSetting == EmptyGameSetting.Playbook() ? EmptyGameSetting.DefaultActionPoint().Points
		: playbookSetting.DefaultActionPoints
			.FirstOrDefault(dap => dap.Action == actionName)
			?.Points
			?? 0;

	public static SpecialAbilitySetting[] GetAvailableStartingAbilities(this GameSetting gameSetting, string heritageName, string playbookName)
	{
		if (gameSetting == EmptyGameSetting.Game() || gameSetting.StartingAbility == null)
			return [];

		var abilities = new StartingSpecialAbilitySetting?[2];
		gameSetting.StartingAbility.AbilitiesByPlaybook.TryGetValue(playbookName, out abilities[0]);
		gameSetting.StartingAbility.AbilitiesByHeritage.TryGetValue(heritageName, out abilities[1]);

#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
		return abilities.Where(setting => setting != null)
			.ToArray();
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
	}

	public static SpecialAbilitySetting? FindAbility(this GameSetting gameSetting, string? expectedPlaybook, PlaybookSpecialAbility? playbookAbility)
	{
		if (gameSetting == EmptyGameSetting.Game() || !expectedPlaybook.HasInk() || playbookAbility == null)
			return null;

		var ability = gameSetting.Playbooks.FirstOrDefault(p => p.Name.Like(expectedPlaybook!))
			?.SpecialAbilities.FirstOrDefault(a => a.Name.Like(playbookAbility.Name));

		if (ability != null)
			return ability;

		return gameSetting.Playbooks.Where(p => !p.Name.Like(expectedPlaybook!))
			.SelectMany(p => p.SpecialAbilities)
			.FirstOrDefault(a => a.Name.Like(playbookAbility.Name))
			?? null;
	}
}
