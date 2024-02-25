using Microsoft.AspNetCore.Components;
using Models.Characters;
using Models.Settings;
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

	//Color StressColor =>
	//	Monitor.Stress.CurrentStress > 6 ? Color.Error
	//	: Monitor.Stress.CurrentStress > 3 ? Color.Warning
	//	: Color.Fill;
}