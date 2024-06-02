using Microsoft.AspNetCore.Components;
using Models.Characters;
using Models.Common;
using Models.Settings;
using UI.Conveniences;

namespace UI.Components.CharacterSheet;
public partial class SheetCreateAbility
{
	private const string CustomVeteranSource = "Custom";

	[Parameter, EditorRequired]
	public Character Character { get; set; } = Character.Empty();

	[Parameter, EditorRequired]
	public GameSetting GameSetting { get; set; } = EmptyGameSetting.Game();

	[Parameter, EditorRequired]
	public SpecialAbilitySetting? ParentSelectedAbility { get; set; }

	[Parameter, EditorRequired]
	public bool IsFixMode { get; set; } = false;

	string[] VeteranSources { get; set; } = [];

	string VeteranSource { get; set; } = string.Empty;

	SpecialAbilitySetting[] AvailableVeteranAbilities { get; set; } = [];

	SpecialAbilitySetting? VeteranSelectedAbility { get; set; }

	Playbook Playbook => Character.Playbook;

	string VeteranAbilityName { get; set; } = string.Empty;

	string CustomAbilityName { get; set; } = string.Empty;

	string CustomAbilityDescription { get; set; } = string.Empty;

	bool IsCreatingAbility { get; set; } = false;

	bool IsCreatingCustom => VeteranSource == CustomVeteranSource;

	bool IsAddCustomButtonEnabled =>
		CustomAbilityName.HasInk() && CustomAbilityDescription.HasInk();

	bool IsAddVeteranButtonEnabled =>
		VeteranSelectedAbility != null;

	protected override void OnParametersSet()
	{
		VeteranAbilityName = GameSetting.GetPlaybookSetting(Playbook.Name)
			.GetAvailableAbilities()
			.LastOrDefault()
			?.Name ?? string.Empty;

		IsCreatingAbility = IsFixMode || (Playbook.Experience.CanLevelUp && (ParentSelectedAbility?.Name.Like(VeteranAbilityName) ?? false));

		VeteranSources = GameSetting.Playbooks.Select(p => p.Name)
			.Where(name => !name.Like(Playbook.Name))
			.Append(CustomVeteranSource)
			.ToArray();

		if (!VeteranSource.HasInk())
			VeteranSource = VeteranSources.First();

		if (!IsCreatingCustom)
			ReloadAvailableVeteranAbilities();

		base.OnParametersSet();
	}

	private void ReloadAvailableVeteranAbilities()
	{
		AvailableVeteranAbilities = GameSetting.GetDisplayableAbilities(Playbook, VeteranSource);
		VeteranSelectedAbility = AvailableVeteranAbilities.FirstOrDefault();
	}

	public void OnVeteranSourceChanged(string veteranSource)
	{
		VeteranSource = veteranSource;

		if (!IsCreatingCustom)
			ReloadAvailableVeteranAbilities();
	}

	public void AddVeteranAbility()
	{
		if (!IsAddVeteranButtonEnabled)
			return;

		if (!Playbook.TakeAbility(VeteranSelectedAbility!))
			return;

		ReloadAvailableVeteranAbilities();

		if (!IsFixMode)
			Playbook.Experience.Points = 0;

		SheetJank.NotifyAbilitiesChanged();
	}

	public void AddCustomAbility()
	{
		if (!IsAddCustomButtonEnabled)
			return;

		if (!Playbook.TakeAbility(new(CustomAbilityName, 1, CustomAbilityDescription)))
		{
			if (Playbook.AbilitiesByName.ContainsKey(CustomAbilityName))
				Toaster.ShowWarning("The name of your custom ability cannot match any playbook ability you already know.");

			return;
		}

		CustomAbilityName = string.Empty;
		CustomAbilityDescription = string.Empty;

		if (!IsFixMode)
			Playbook.Experience.Points = 0;

		SheetJank.NotifyAbilitiesChanged();
		return;
	}
}