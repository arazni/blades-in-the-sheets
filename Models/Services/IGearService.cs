using Models.Characters;

namespace Models.Services;

public interface IGearService
{
	GearItem[] LoadAvailableGearFromPlaybook(PlaybookOption playbooks);
}
