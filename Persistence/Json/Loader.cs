using Domain.Characters;
using Newtonsoft.Json;
using Persistence.Json.DataModels;

namespace Persistence.Json;

public class Loader : ILoader
{
	private const string FriendsFilePrefix = "friends";
	private const string PlaybookAbilitiesFilePrefix = "playbook.abilities";
	private const string GearLoadFilePrefix = "gear.load";
	private const string StandardGearLoadFileSuffix = "standard";
	private readonly IFileReader fileReader;

	public Loader(IFileReader fileReader)
	{
		this.fileReader = fileReader;
	}

	public async Task<RolodexFriend[]> LoadAvailableFriendsAsync(string identifierOrPlaybook)
	{
		var file = await ReadFile(FriendsFilePrefix, identifierOrPlaybook);

		var entries = JsonConvert.DeserializeObject<string[]>(file)
			?? throw new JsonException($"Wasn't able to deserialize {identifierOrPlaybook} to {nameof(RolodexFriend)}[]");

		return entries.Select(entry => new RolodexFriend(entry))
			.ToArray();
	}

	public async Task<PlaybookSpecialAbility[]> LoadAvailableAbilitiesAsync(string identifierOrPlaybook)
	{
		if (!Enum.TryParse(identifierOrPlaybook, out Source source))
			source = Source.Custom;

		var file = await ReadFile(PlaybookAbilitiesFilePrefix, identifierOrPlaybook);

		var data = JsonConvert.DeserializeObject<AvailablePlaybookSpecialAbilityData[]>(file)
			?? throw new JsonException($"Wasn't able to deserialize {identifierOrPlaybook} to {nameof(PlaybookSpecialAbility)}[]");

		return data.Select(data => new PlaybookSpecialAbility(data.Name, data.Description, data.TimesTakeable, source))
			.ToArray();
	}

	public async Task<GearItem[]> LoadAvailableItemsAsync(string identifierOrPlaybook)
	{
		if (!Enum.TryParse(identifierOrPlaybook, out GearItem.Sources source))
			source = GearItem.Sources.Custom;

		var file = await ReadFile(GearLoadFilePrefix, identifierOrPlaybook);

		var specificData = JsonConvert.DeserializeObject<AvailableGearItemData[]>(file)
			?? throw new JsonException($"Wasn't able to deserialize {identifierOrPlaybook} to {nameof(GearItem)}[]");

		file = await ReadFile(GearLoadFilePrefix, StandardGearLoadFileSuffix);

		var standardData = JsonConvert.DeserializeObject<AvailableGearItemData[]>(file)
			?? throw new JsonException($"Wasn't able to deserialize {StandardGearLoadFileSuffix} to {nameof(GearItem)}[]");

		var specificGear = specificData.Select(d => new GearItem(d.Bulk, d.Name, source));
		var standardGear = standardData.Select(d => new GearItem(d.Bulk, d.Name, GearItem.Sources.Standard));

		return specificGear.Concat(standardGear)
			.ToArray();
	}

	private async Task<string> ReadFile(string prefix, string identifierOrPlaybook)
	{
		var fileName = ToFileName(prefix, identifierOrPlaybook);
		return await this.fileReader.ReadFile(fileName);
	}

	private static string ToFileName(string prefix, string identifierOrPlaybook) => $"{prefix}.{identifierOrPlaybook}.json".ToLower();
}
