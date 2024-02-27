namespace UI.Pages;
public partial class Demo : CharacterSheet
{
	protected override async Task Save()
	{
		await Task.Delay(1);
		Toaster.ShowSuccess("Pretended to save!");
	}

	protected override async Task OnParametersSetAsync()
	{
		Character = await DemoReader.GetDemoCharacter();
		GameSetting = await Loader.LoadSetting(Character.GameName);
		Id = Character.Id;
	}
}