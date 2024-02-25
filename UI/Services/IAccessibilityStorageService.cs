using Models.Settings;

namespace UI.Services;
public interface IAccessibilityStorageService
{
	Task<AccessibilitySetting?> LoadAccessibilitySetting();
	Task SaveAccessibilitySetting(AccessibilitySetting setting);
}