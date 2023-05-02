namespace Models.Characters;

public class Monitor
{
	public Monitor(int healingClockSize)
	{
		Harm = new(healingClockSize);
	}

	public MonitorStress Stress { get; private set; } = new();

	public MonitorTrauma Trauma { get; private set; } = new();

	public MonitorHarm Harm { get; private set; }

	public static Monitor Empty() => new(4);
}
