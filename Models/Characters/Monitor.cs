namespace Models.Characters;

public class Monitor
{
	public MonitorStress Stress { get; private set; } = new();

	public MonitorTrauma Trauma { get; private set; } = new();

	public MonitorHarm Harm { get; private set; } = new();
}
