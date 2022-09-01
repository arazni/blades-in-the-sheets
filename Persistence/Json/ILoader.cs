using Domain.Characters;

namespace Persistence.Json;

public interface ILoader
{
	Task<PlaybookSpecialAbility[]> LoadAvailableAbilitiesAsync(string identifierOrPlaybook);

	Task<Dictionary<PlaybookOption, PlaybookSpecialAbility[]>> LoadAvailableAbilitiesByOptionAsync();

	Task<RolodexFriend[]> LoadAvailableFriendsAsync(string identifierOrPlaybook);

	Task<GearItem[]> LoadAvailableItemsAsync(string identifierOrPlaybook);
}