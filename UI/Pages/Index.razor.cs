using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;
using Models.Common;
using Models.Settings;
using Persistence.Json.DataModels;
using System.Text;
using UI.ViewModels;
using static Models.Settings.Constants;
using static UI.Constants;

namespace UI.Pages;

public partial class Index
{
	IJSObjectReference? module;

	IReadOnlyCollection<VIndexCharacter> Characters { get; set; } = Array.Empty<VIndexCharacter>();

	GameFile[] AvailableGameFiles { get; set; } = [];

	GameFile[] SelectableGameFiles { get; set; } = [];

	string[] AvailableLanguages { get; set; } = [];

	GameFile SelectedGameFile { get; set; } = new(Games.BladesInTheDark, Games.BladesInTheDarkStem, Languages.English);

	string SelectedLanguage { get; set; } = Languages.English;

	GameSetting[] GameSettings { get; set; } = [];

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
			module = await JS.InvokeAsync<IJSObjectReference>("import", "./Pages/Index.razor.js");

		await base.OnAfterRenderAsync(firstRender);
	}

	protected override async Task OnInitializedAsync()
	{
		await LoadCharacters();

		AvailableGameFiles = await Loader.LoadGameFiles();
		Console.WriteLine("available count: " + AvailableGameFiles.Length);

		AvailableLanguages = AvailableGameFiles.Select(gf => gf.Language)
			.Distinct()
			.ToArray();

		var gameSettingLoads = AvailableGameFiles.Select(Loader.LoadSetting);

		GameSettings = await Task.WhenAll(gameSettingLoads);

		OnSelectedLanguageChanged(Languages.English);

		await base.OnInitializedAsync();
	}

	async Task LoadCharacters()
	{
		var characters = await Storage.LoadAll();

		if (!characters.Any())
		{
			Characters = [];
			return;
		}

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

		await module!.InvokeVoidAsync("downloadFileFromStream", $"{character.Name} BitS.json", streamRef);
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
		var gameNameMatches = GameSettings.Where(gf => gf.Name.Like(vCharacter.GameName));

		if (gameNameMatches.IsNullOrEmpty())
			return vCharacter.Blurb(EmptyGameSetting.Game());

		var languageMatch = gameNameMatches.FirstOrDefault(gf => gf.Language.Like(vCharacter.Language));

		if (languageMatch is not null)
			return vCharacter.Blurb(languageMatch);

		return vCharacter.Blurb(gameNameMatches.First());
	}

	string DownloadModeLabel =>
		IsDownloadMode ? "Disable download mode"
		: "Enable download mode";

	static string DownloadCharacterLabel(string characterName) =>
		$"Export {characterName}";

	static string DeleteCharacterLabel(string characterName) =>
		$"Delete {characterName}";

	void OnSelectedLanguageChanged(string language)
	{
		SelectedLanguage = language;

		if (!AvailableGameFiles.Any())
			return;

		SelectableGameFiles = AvailableGameFiles.Where(gf => gf.Language.Like(language)).ToArray();
		SelectedGameFile = SelectableGameFiles.First();
	}
}
