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

	public static PlaybookSpecialAbility[] GetAvailableAbilities(this PlaybookSetting playbookSetting) =>
		playbookSetting == EmptyGameSetting.Playbook() ? EmptyGameSetting.PlaybookSpecialAbilities()
		: playbookSetting.SpecialAbilities
			.Select(data => new PlaybookSpecialAbility(data.Name, data.Description, data.TimesTakeable))
			.ToArray();

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

	public static PlaybookSpecialAbility[] GetAvailableStartingAbilities(this GameSetting gameSetting, string heritageName, string playbookName)
	{
		if (gameSetting == EmptyGameSetting.Game() || gameSetting.StartingAbility == null)
			return EmptyGameSetting.PlaybookSpecialAbilities();

		Console.WriteLine("heritageName: " + heritageName);
		Console.WriteLine("playbookName: " + playbookName);
		var abilities = new StartingSpecialAbilitySetting?[2];
		gameSetting.StartingAbility.AbilitiesByPlaybook.TryGetValue(playbookName, out abilities[0]);
		gameSetting.StartingAbility.AbilitiesByHeritage.TryGetValue(heritageName, out abilities[1]);

		foreach (var x in abilities) Console.WriteLine(x);

		return abilities.Where(setting => setting != null)
			.Select(setting => new PlaybookSpecialAbility(setting!.Name, setting.Description, 1))
			.ToArray();
	}
}
