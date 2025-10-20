using Models.Common;
using Models.Settings;

namespace Models.Characters;

public partial class Character // Armor
{
	private static readonly string[] StandardArmors = ["Armor", "Armure", "Доспех"];
	private static readonly string[] HeavyArmors = ["+Heavy", "+Lourde", "+Тяжёлый"];
	private static readonly string[] SpecialArmors = ["special armor", "armure spéciale", "особая защита", "особую защиту"];

	private bool isStandardArmorUsed;
	private bool isHeavyArmorUsed;
	private bool isSpecialArmorUsed;

	// Dirty fix since we can't know armor rules for other games.
	private string[] HeavyArmorSearchText =>
		GameName == Constants.Games.BladesInTheDark ? HeavyArmors
		: StandardArmors;

	public bool HasArmor =>
		Gear.Loadout.Select(item => item.Name)
			.HasAny(StandardArmors);

	public bool HasHeavyArmor =>
		Gear.Loadout.Select(item => item.Name)
			.HasAny(HeavyArmorSearchText);

	public bool HasSpecialArmor =>
		Playbook.Abilities.Select(ability => ability.Description)
			.Any(description => description.EmbedsAny(SpecialArmors));

	public void UseStandardArmor()
	{
		if (!HasArmor)
			throw new InvalidOperationException("Tried to use armor that doesn't exist");

		this.isStandardArmorUsed = true;
	}

	public void UseHeavyArmor()
	{
		if (!HasHeavyArmor)
			throw new InvalidOperationException("Tried to use heavy armor that doesn't exist");

		this.isHeavyArmorUsed = true;
	}

	public void UseSpecialArmor()
	{
		if (!HasSpecialArmor)
			throw new InvalidOperationException("Tried to use special armor that doesn't exist");

		this.isSpecialArmorUsed = true;
	}

	public bool IsStandardArmorUsed =>
		this.isStandardArmorUsed;

	public bool IsHeavyArmorUsed =>
		this.isHeavyArmorUsed;

	public bool IsSpecialArmorUsed =>
		this.isSpecialArmorUsed;

	public void RestoreStandardArmor() =>
		this.isStandardArmorUsed = false;

	public void RestoreHeavyArmor() =>
		this.isHeavyArmorUsed = false;

	public void RestoreSpecialArmor() =>
		this.isSpecialArmorUsed = false;
}
