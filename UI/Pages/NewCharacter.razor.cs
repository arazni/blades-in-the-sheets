using Microsoft.AspNetCore.Components;
using Models.Settings;

namespace UI.Pages;

public partial class NewCharacter
{
	[Parameter]
	public string? GameFileStem { get; set; }

	GameSetting GameSetting { get; set; } = EmptyGameSetting.Game();

	string GameName => GameSetting.Name;

	protected override async Task OnParametersSetAsync()
	{
		GameFileStem ??= Models.Settings.Constants.Games.BladesInTheDarkStem;

		GameSetting = await Loader.LoadSetting(GameFileStem);

		options = GameSetting.Playbooks.Select(p => (p.Name, p.Hook))
			.ToArray();

		await base.OnParametersSetAsync();
	}

	private (string, string)[] options = Array.Empty<(string, string)>();
}
