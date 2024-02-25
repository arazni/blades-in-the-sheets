using Models.Settings;

namespace UI.Services;
public interface IAccessibilitySettingService
{
	event Action<AccessibilitySetting>? SettingChanged;

	Task<AccessibilitySetting.RatingImplementation> GetRatingImplementation();
	Task SetAccessibilitySetting(AccessibilitySetting setting);
}