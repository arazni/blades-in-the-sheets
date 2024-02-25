using Microsoft.AspNetCore.Components;
using Models.Characters;
using Models.Common;
using UI.Services;
using Monitor = Models.Characters.Monitor;

namespace UI.Components.CharacterSheet;
public partial class SheetMonitorTrauma
{
	[Parameter, EditorRequired]
	public bool IsFixMode { get; set; }

	[Parameter, EditorRequired]
	public Monitor Monitor { get; set; } = Monitor.Empty();

	[Parameter, EditorRequired]
	public IReadOnlyCollection<string> TraumaOptions { get; set; } = Array.Empty<string>();

	bool DisableTraumaSelect =>
		Monitor.Stress.CurrentStress != MonitorStress.MaxStress
		&& !IsFixMode
		|| Monitor.Trauma.IsRetired;

	bool DisableAddTrauma =>
		DisableTraumaSelect
		|| !SelectedTrauma.HasInk()
		|| Monitor.Trauma.IsRetired;

	string SelectedTrauma { get; set; } = string.Empty;

	protected override void OnParametersSet()
	{
		if (TraumaOptions.Any())
			ResetTraumaSelect();

		base.OnParametersSet();
	}

	private void ResetTraumaSelect()
	{
		SelectedTrauma = TraumaOptions.Except(Monitor.Trauma.Traumas).First();
	}

	void AddTrauma()
	{
		if (!SelectedTrauma.HasInk())
			return;

		var success = Monitor.Trauma.Add(SelectedTrauma);

		if (!success)
			return;

		if (!IsFixMode)
		{
			Monitor.Stress.CurrentStress = 0;
			SheetJank.NotifyTraumaChanged();
		}

		ResetTraumaSelect();
	}

	void RemoveTrauma(string trauma)
	{
		Monitor.Trauma.Remove(trauma);
	}

	static string RemoveTraumaAriaLabel(string trauma) =>
		$"Remove {trauma}";
}