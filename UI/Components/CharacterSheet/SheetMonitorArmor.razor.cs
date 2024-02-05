using Microsoft.AspNetCore.Components;
using Models.Characters;
using UI.Services;

namespace UI.Components.CharacterSheet;
public sealed partial class SheetMonitorArmor
{
	[Parameter, EditorRequired]
	public Character Character { get; set; } = Character.Empty();

	protected override void OnInitialized()
	{
		SheetJank.GearChanged += StateHasChanged;
		SheetJank.AbilitiesChanged += StateHasChanged;

		base.OnInitialized();
	}

	void StandardArmorChecked(bool isChecked)
	{
		if (isChecked)
			Character.UseStandardArmor();
		else
			Character.RestoreStandardArmor();
	}

	void HeavyArmorChecked(bool isChecked)
	{
		if (isChecked)
			Character.UseHeavyArmor();
		else
			Character.RestoreHeavyArmor();
	}

	void SpecialArmorChecked(bool isChecked)
	{
		if (isChecked)
			Character.UseSpecialArmor();
		else
			Character.RestoreSpecialArmor();
	}

	public void Dispose()
	{
		SheetJank.GearChanged -= StateHasChanged;
		SheetJank.AbilitiesChanged -= StateHasChanged;
	}
}