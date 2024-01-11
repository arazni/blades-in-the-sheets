using Microsoft.AspNetCore.Components;
using Models.Characters;
using Models.Settings;
using static UI.Constants;

namespace UI.Pages;
public partial class CharacterCreation
{
	[Parameter]
	public string Id { get; set; } = string.Empty;

	[Parameter]
	public string GameName { get; set; } = string.Empty;

	[Parameter]
	public string PlaybookName { get; set; } = string.Empty;

	public Character Character { get; private set; } = Character.Empty();

	public GameSetting GameSetting { get; private set; } = EmptyGameSetting.Game();

	public PlaybookSetting PlaybookSetting => GameSetting.GetPlaybookSetting(Character.Playbook.Name);

	public RolodexCreation RolodexCreation = new();

	protected override async Task OnParametersSetAsync()
	{
		GameSetting = await Loader.LoadSetting(GameName);
		Character = Coordinator.InitializeCharacter(GameSetting, PlaybookName);

		await base.OnParametersSetAsync();
	}

	public async Task SaveAsync()
	{
		Character.Rolodex.ReplaceFriends(RolodexCreation);

		await Storage.Save(Character);
		Navigator.NavigateTo(Paths.Sheet(Character.Id));
	}
}