using Domain.Common;

namespace Domain.Characters;

public partial class Character // Armor
{
	private const string Standard = "Armor";
	private const string Heavy = "+Heavy";
	private const string Special = "special armor";

	private bool isStandardArmorUsed;
	private bool isHeavyArmorUsed;
	private bool isSpecialArmorUsed;

	public bool HasArmor =>
		Gear.Loadout.Select(item => item.Name)
			.Has(Standard);

	public bool HasHeavyArmor =>
		Gear.Loadout.Select(item => item.Name)
			.Has(Heavy);

	public bool CanHaveSpecialArmor =>
		Playbook.Abilities.Select(ability => ability.Description)
			.Any(description => description.Embeds(Special));

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
		if (!CanHaveSpecialArmor)
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
