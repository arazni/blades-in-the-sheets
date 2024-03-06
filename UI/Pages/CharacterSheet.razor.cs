using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Models.Characters;
using Models.Settings;
using Toolbelt.Blazor.HotKeys2;

namespace UI.Pages;
public partial class CharacterSheet
{
	[Parameter, EditorRequired]
	public string Id { get; set; } = string.Empty;

	public GameSetting GameSetting { get; set; } = EmptyGameSetting.Game();

	public Character Character { get; set; } = Character.Empty();

	PlaybookSetting PlaybookSetting => GameSetting.GetPlaybookSetting(Character.Playbook.Name);

	private bool isSaving = false;

	private HotKeysContext? hotKeysContext;

	Icon SaveIcon => isSaving ? new Icons.Filled.Size32.CheckmarkCircle()
		: new Icons.Filled.Size28.Save();

	protected override void OnInitialized()
	{
		this.hotKeysContext = Hotkeys.CreateContext()
			.Add(ModCode.Ctrl, Code.S, HackSave, "Save character sheet");

		base.OnInitialized();
	}

	protected override async Task OnParametersSetAsync()
	{
		Character = await Storage.Load(Id);
		GameSetting = await Loader.LoadSetting(Character.GameName, Character.Language);

		await base.OnParametersSetAsync();
	}

	async ValueTask HackSave() => await Save();

	protected async virtual Task Save()
	{
		isSaving = true;
		try
		{
			await Storage.Save(Character!);
		}
		catch (Exception ex)
		{
			Toaster.ShowError($"Failed to save, contact arazni on github and let her know: {ex}");
			Console.Write(ex);
			isSaving = false;
			return;
		}

		Toaster.ShowSuccess("Saved!");
		isSaving = false;
	}

	public void Dispose()
	{
		this.hotKeysContext?.Dispose();
		GC.SuppressFinalize(this);
	}
}