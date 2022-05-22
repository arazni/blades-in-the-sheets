using Domain.Common;

namespace Domain.Characters;

public class MonitorArmor
{
	private const string Armor = "Armor";
	private const string Heavy = "+Heavy";
	private const string Special = "special armor";

	public static Func<Gear, bool> GetHasArmor =>
		gear => gear.Loadout.Select(item => item.Name)
			.Has(Armor);

	public static Func<Gear, bool> GetHasHeavy =>
		gear => gear.Loadout.Select(item => item.Name)
			.Has(Heavy);

	public static Func<Playbook, bool> GetHasSpecial =>
		playbook => playbook.Abilities.Select(ability => ability.Description)
			.Any(description => description.Embeds(Special));
}
