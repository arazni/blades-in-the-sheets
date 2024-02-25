using Microsoft.AspNetCore.Components;
using Models.Characters;
using Models.Game;
using UI.Services;
using Monitor = Models.Characters.Monitor;

namespace UI.Components.CharacterSheet;
public sealed partial class SheetMonitorRecovery
{
	[Parameter, EditorRequired]
	public Monitor Monitor { get; set; } = Monitor.Empty();

	[Parameter, EditorRequired]
	public bool IsFixMode { get; set; }

	RecoverySelectable[] RecoverySelectables { get; } =
	[
		new(1, "1-3: +1 Tick"),
		new(2, "4-5: +2 Ticks"),
		new(3, "6: +3 Ticks"),
		new(5, "Critical: +5 Ticks")
	];

	MonitorHarm Harm =>
		Monitor.Harm;

	RolloverClock HealingClock =>
		Harm.HealingClock;

	int MaxRecovery =>
		HealingClock.Size;

	int RecoveryAmount = 1;

	void Heal()
	{
		Harm.Heal();
		SheetJank.NotifyHarmChanged();
	}

	string CurrentRecoveryProgressLabel =>
		$"Current recovery progress: {HealingClock.Time} of {HealingClock.Size}";

	string SetRecoveryProgressLabel =>
		$"Set recovery progress: {HealingClock.Time} of {HealingClock.Size}";

	string ExtraRecoveryProgressLabel =>
		$"Rollover recovery progress: {HealingClock.Rollover}";
}

record RecoverySelectable(int Value, string Text);