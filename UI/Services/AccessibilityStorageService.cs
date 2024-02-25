using Blazored.LocalStorage;
using Models.Settings;
using Persistence.Json;

namespace UI.Services;

public class AccessibilityStorageService(ILocalStorageService localStorageService, ISerializer serializer) : IAccessibilityStorageService
{
	readonly ILocalStorageService storageService = localStorageService;
	readonly ISerializer serializer = serializer;
	const string AccessibilityKey = "accessibility";

	public async Task SaveAccessibilitySetting(AccessibilitySetting setting)
	{
		var json = this.serializer.Serialize(setting);
		await this.storageService.SetItemAsStringAsync(AccessibilityKey, json);
	}

	public async Task<AccessibilitySetting?> LoadAccessibilitySetting()
	{
		var json = await this.storageService.GetItemAsStringAsync(AccessibilityKey);

		if (json == null)
			return null;

		return this.serializer.DeserializeAccessibility(json);
	}
}
