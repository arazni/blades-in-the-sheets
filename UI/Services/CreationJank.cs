namespace UI.Services;

public class CreationJank
{
	public event Action? HeritageChanged;
	public void NotifyHeritageChanged() => HeritageChanged?.Invoke();
}
