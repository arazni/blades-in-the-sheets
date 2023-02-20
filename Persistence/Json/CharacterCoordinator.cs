using Models.Characters;

namespace Persistence.Json;

public class CharacterCoordinator : ICharacterCoordinator
{
	private readonly ILoader loader;

	public CharacterCoordinator(ILoader loader)
	{
		this.loader = loader;
	}

	public async Task<Character> InitializeCharacter(PlaybookOption option)
	{
		var character = new Character(option);
		await InitializeAvailableGear(character);
		return character;
	}

	private async Task<Character> InitializeAvailableGear(Character character)
	{
		var gearItems = await this.loader.LoadAvailableItemsAsync(character.Playbook.Option.ToString());

		foreach (var item in gearItems)
			character.Gear.AddAvailableItem(item);

		return character;
	}
}
