namespace UI.Services;

public class SheetJank
{
	public event Action? HarmChanged;
	public void NotifyHarmChanged() => HarmChanged?.Invoke();

	public event Action? TraumaChanged;
	public void NotifyTraumaChanged() => TraumaChanged?.Invoke();

	public event Action<string>? AttributeChanged;
	public void NotifyAttributeChanged(string attributeName) =>
		AttributeChanged?.Invoke(attributeName);

	public event Action? GearChanged;
	public void NotifyGearChanged() => GearChanged?.Invoke();

	public event Action? SatchelChanged;
	public void NotifySatchelChanged() => SatchelChanged?.Invoke();

	public event Action? AbilitiesChanged;
	public void NotifyAbilitiesChanged() => AbilitiesChanged?.Invoke();
}
