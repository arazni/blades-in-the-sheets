using Microsoft.AspNetCore.Components;
using Models.Characters;
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

	public IReadOnlyCollection<string> AvailableTraumaOptions =>
		TraumaOptions.Except(Monitor.Trauma.Traumas)
			.ToArray();

	bool DisableTraumaSelect =>
		Monitor.Stress.CurrentStress != MonitorStress.MaxStress
		&& !IsFixMode;

	bool DisableAddTrauma =>
		DisableTraumaSelect
		|| SelectedTrauma == null;

	private string? selectedTrauma;

	string? SelectedTrauma
	{
		get => Monitor.Stress.CurrentStress != MonitorStress.MaxStress && !IsFixMode ? null
			: selectedTrauma;
		set => selectedTrauma = value;
	}

	void AddTrauma()
	{
		if (SelectedTrauma == null)
			return;

		var success = Monitor.Trauma.Add(SelectedTrauma);

		if (!success)
			return;

		if (!IsFixMode)
		{
			Monitor.Stress.CurrentStress = 0;
			SheetJank.NotifyTraumaChanged();
		}

		SelectedTrauma = null;
	}

	void RemoveTrauma(string trauma)
	{
		Monitor.Trauma.Remove(trauma);
	}

	static string RemoveTraumaAriaLabel(string trauma) =>
		$"Remove {trauma}";
}