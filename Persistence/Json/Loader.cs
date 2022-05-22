using Domain.Characters;
using Persistence.Json.DataModels;
using Newtonsoft.Json;

namespace Persistence.Json;

public class Loader : ILoader
{
	private readonly string directoryPath;
	private const string FriendsFilePrefix = "friends";
	private const string PlaybookAbilitiesFilePrefix = "playbook.abilities";
	private const string GearLoadFilePrefix = "gear.load";
	private const string StandardGearLoadFileSuffix = "standard";

	public Loader()
	{
		this.directoryPath = GetDataDirectoryPath();
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

	private static string GetDataDirectoryPath()
	{
		DirectoryInfo directory = new(Directory.GetCurrentDirectory());

		//while(directory.Parent?.Name != nameof(Persistence))
		//	directory = directory.Parent ?? throw new DirectoryNotFoundException("Persistence is not in the path");

		directory = directory.EnumerateDirectories()
			.FirstOrDefault(d => d.Name == "Data")
			?? throw new DirectoryNotFoundException($"Data is not a subdirectory of {directory.FullName}");

		return directory.FullName;
	}

	private async Task<string> ReadFile(string prefix, string identifierOrPlaybook)
	{
		var path = Path.Combine(this.directoryPath, $"{prefix}.{identifierOrPlaybook.ToLower()}.json");

		var text = await File.ReadAllTextAsync(path);
			//?? throw new FileNotFoundException($"Could not find {path}");

		return text;
	}
}
