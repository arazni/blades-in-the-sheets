namespace UI.Services;

public class SheetJank
{
	public event Action? HarmChanged;
	public void NotifyHarmChanged() => HarmChanged?.Invoke();

	public event Action? InsightChanged;
	public void NotifyInsightChanged() => InsightChanged?.Invoke();

	public event Action? ProwessChanged;
	public void NotifyProwessChanged() => ProwessChanged?.Invoke();

	public event Action? ResolveChanged;
	public void NotifyResolveChanged() => ResolveChanged?.Invoke();

	public event Action? GearChanged;
	public void NotifyGearChanged() => GearChanged?.Invoke();

	public event Action? SatchelChanged;
	public void NotifySatchelChanged() => SatchelChanged?.Invoke();
}
