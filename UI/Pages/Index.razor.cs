using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;
using Models.Settings;
using Persistence.Json.DataModels;
using System.Collections.Immutable;
using System.Text;
using UI.ViewModels;
using static Models.Settings.Constants;
using static UI.Constants;

namespace UI.Pages;

public partial class Index
{
	IReadOnlyCollection<VIndexCharacter> Characters { get; set; } = Array.Empty<VIndexCharacter>();

	GameFile[] AvailableGameFiles { get; set; } = [];

	GameFile SelectedGameFile { get; set; } = new(Games.BladesInTheDark, Games.BladesInTheDarkStem);

	ImmutableDictionary<string, GameSetting> GameSettingsByName = ImmutableDictionary<string, GameSetting>.Empty;

	protected override async Task OnInitializedAsync()
	{
		await LoadCharacters();

		AvailableGameFiles = await Loader.LoadGameFiles();

		var gameSettingLoads = AvailableGameFiles.Select(gameFile => Loader.LoadSetting(gameFile));

		var gameSettings = await Task.WhenAll(gameSettingLoads);

		GameSettingsByName = gameSettings.ToImmutableDictionary
		(
			gameSetting => gameSetting.Name,
			gameSetting => gameSetting
		);

		await base.OnInitializedAsync();
	}

	async Task LoadCharacters()
	{
		var characters = await Storage.LoadAll();

		if (!characters.Any())
			return;

		Characters = characters.Select(character => new VIndexCharacter(character))
			.OrderBy(c => (c.GameName, c.Alias))
			.ToArray();
	}

	string HeaderText => Characters.Any() ? "Your scoundrels await:"
		: "A Blades in the Dark Character Sheet App";

	bool IsDeleteMode { get; set; }

	bool IsDownloadMode { get; set; }

	string DownloadModeClass => Classes.Display(IsDownloadMode);

	string NotDownloadModeClass => Classes.DisplayNone(IsDownloadMode);

	string DeleteModeClass => Classes.Display(IsDeleteMode);

	string NotDeleteModeClass => Classes.DisplayNone(IsDeleteMode);

	async Task Delete(string id)
	{
		await Storage.Delete(id);
		await LoadCharacters();
	}

	async Task Download(VIndexCharacter character)
	{
		var json = await Storage.GetFile(character.Id);
		var bytes = Encoding.Unicode.GetBytes(json);
		var memory = new MemoryStream(bytes);
		using var streamRef = new DotNetStreamReference(memory);

		// await module!.InvokeVoidAsync("downloadFileFromStream", $"{character.Name} BitS.json", streamRef);
	}

	async Task UploadFiles(FluentInputFileEventArgs fileEvent)
	{
		var buffer = new byte[fileEvent.Size];
		await using var fs = fileEvent.Stream;
		await fs!.ReadAsync(buffer);
		var json = Encoding.Unicode.GetString(buffer);
		await Storage.PutFile(json);
	}

	async Task UploadComplete()
	{
		await LoadCharacters();
	}

	string New() => SelectedGameFile == null ? ""
		: Paths.NewCharacter(SelectedGameFile.Stem);

	string Blurb(VIndexCharacter vCharacter)
	{
		if (!GameSettingsByName.TryGetValue(vCharacter.GameName, out var gameSetting))
			return vCharacter.Blurb(EmptyGameSetting.Game());

		return vCharacter.Blurb(gameSetting);
	}

	string DownloadModeLabel =>
		IsDownloadMode ? "Disable download mode"
		: "Enable download mode";

	static string DownloadCharacterLabel(string characterName) =>
		$"Export {characterName}";

	static string DeleteCharacterLabel(string characterName) =>
		$"Delete {characterName}";
}
