using Models.Settings;

namespace UI.Services;

public class AccessibilitySettingService(IAccessibilityStorageService storageService) : IAccessibilitySettingService
{
	bool hasLoaded = false;
	AccessibilitySetting setting = new();
	readonly IAccessibilityStorageService storageService = storageService;

	public async Task<AccessibilitySetting.RatingImplementation> GetRatingImplementation()
	{
		if (hasLoaded)
			return this.setting.Rating;

		this.setting = await this.storageService.LoadAccessibilitySetting()
			?? new();

		this.hasLoaded = true;
		return this.setting.Rating;
	}

	public async Task SetAccessibilitySetting(AccessibilitySetting setting)
	{
		this.setting = setting;
		await this.storageService.SaveAccessibilitySetting(setting);
		hasLoaded = true;
		NotifySettingChanged(this.setting);
	}

	public event Action<AccessibilitySetting>? SettingChanged;

	void NotifySettingChanged(AccessibilitySetting setting) =>
		SettingChanged?.Invoke(setting);
}
