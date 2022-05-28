using Domain.Characters;

namespace Domain.Services;

public interface IGearService
{
	GearItem[] LoadAvailableGearFromPlaybook(PlaybookOption playbooks);
}
