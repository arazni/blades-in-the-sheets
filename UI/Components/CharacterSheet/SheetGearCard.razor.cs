using Microsoft.AspNetCore.Components;
using Models.Characters;
using Models.Common;
using Models.Settings;
using UI.Services;

namespace UI.Components.CharacterSheet;

public partial class SheetGearCard
{
	[Parameter, EditorRequired]
	public Character Character { get; set; } = Character.Empty();

	[Parameter, EditorRequired]
	public GameSetting GameSetting { get; set; } = EmptyGameSetting.Game();

	public Gear Gear => Character.Gear;

	public string BulkSynonym => GameSetting.Thesaurus.Bulk;

	public GearExtraDescriptionSetting[] GearWithExtraDescriptions => GameSetting.ExtraDescription?.GearDescriptions
		?? EmptyGameSetting.ExtraDescription().GearDescriptions;

	public bool IsFixMode { get; set; }

	bool CommitmentLocked { get; set; } = false;

	bool RadioCommitmentsEnabled => IsFixMode || !CommitmentLocked;

	public void CommitGear(GearItem item)
	{
		var isSuccessful = Character.CommitGear(item);

		if (isSuccessful)
			SheetJank.NotifyGearChanged();
	}

	public void ClearCommitments()
	{
		var isSuccessful = Character.ClearCommitments();

		if (isSuccessful)
			SheetJank.NotifyGearChanged();
	}

	public void UncommitGear(GearItem item)
	{
		var isSuccessful = Character.UncommitGear(item);

		if (isSuccessful)
			SheetJank.NotifyGearChanged();
	}

	NewItem AddableItem { get; } = new();

	bool IsAddItemEnabled => AddableItem.Name.HasInk() && Gear.CanAddAvailableItem(AddableItem.Name);

	static string CommitmentButtonLabel(GearItem item) => $"Uncommit {item.Name} ({item.Bulk})";

	void AddItem()
	{
		if (!IsAddItemEnabled)
			return;

		var item = new GearItem(AddableItem.Bulk, AddableItem.Name);
		Gear.AddAvailableItem(item);
		AddableItem.Reset();
	}

	private class NewItem
	{
		public int Bulk { get; set; } = 0;
		public string Name { get; set; } = string.Empty;

		public void Reset()
		{
			Bulk = 0;
			Name = string.Empty;
		}
	}
}