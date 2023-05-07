using Models.Characters;
using Models.Settings;
using Persistence.Json.Migrations;

namespace Persistence.Json;

public class CharacterCoordinator : ICharacterCoordinator
{
	private readonly ILoader loader;

	public CharacterCoordinator(ILoader loader)
	{
		this.loader = loader;
	}

	public Character InitializeCharacter(GameSetting gameSetting, string playbookName)
	{
		var playbookSetting = gameSetting.GetPlaybookSetting(playbookName);

		var character = new Character(gameSetting, playbookName, IMigrationHandler.MaxVersion);

		InitializeAvailableGear(gameSetting, playbookSetting, character);
		return character;
	}

	public async Task<Character> InitializeCharacter(string gameName, string playbookOption)
	{
		var gameSetting = await this.loader.LoadSetting(gameName);
		return InitializeCharacter(gameSetting, playbookOption);
	}

	private static Character InitializeAvailableGear(GameSetting gameSetting, PlaybookSetting playbookSetting, Character character)
	{
		var items = LoadAvailableItems(gameSetting, playbookSetting);

		foreach (var item in items)
			character.Gear.AddAvailableItem(item);

		return character;
	}

	private static GearItem[] LoadAvailableItems(GameSetting gameSetting, PlaybookSetting playbookSetting)
	{
		var specificGear = playbookSetting.Items.Select(i => new GearItem(i.Bulk, i.Name));
		var standardGear = gameSetting.SharedItems.Select(i => new GearItem(i.Bulk, i.Name));

		return specificGear.Concat(standardGear)
			.ToArray();
	}
}
