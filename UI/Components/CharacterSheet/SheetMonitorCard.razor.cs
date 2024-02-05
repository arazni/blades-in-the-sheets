using Microsoft.AspNetCore.Components;
//using Microsoft.FluentUI.AspNetCore.Components;
using Models.Characters;
using Models.Settings;
//using UI.Services;
using Monitor = Models.Characters.Monitor;

namespace UI.Components.CharacterSheet;
public partial class SheetMonitorCard
{
	[Parameter, EditorRequired]
	public Character Character { get; set; } = Character.Empty();

	[Parameter, EditorRequired]
	public GameSetting GameSetting { get; set; } = EmptyGameSetting.Game();

	public Monitor Monitor => Character.Monitor;

	bool IsFixMode { get; set; } = false;

	//string[] StressLabels { get; } =
	//	Enumerable.Range(0, 10)
	//		.Select(i => i.ToString())
	//		.ToArray();

	//Color StressColor =>
	//	Monitor.Stress.CurrentStress > 6 ? Color.Error
	//	: Monitor.Stress.CurrentStress > 3 ? Color.Warning
	//	: Color.Fill;

	//protected override void OnInitialized()
	//{
	//	SheetJank.TraumaChanged += StateHasChanged;

	//	base.OnInitialized();
	//}

	//public void Dispose()
	//{
	//	SheetJank.TraumaChanged -= StateHasChanged;
	//}
}