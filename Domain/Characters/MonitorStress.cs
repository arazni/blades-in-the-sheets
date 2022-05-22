using Domain.Common;

namespace Domain.Characters;

public class MonitorStress
{
	private readonly BoundedInteger currentStress = new(0, MaxStress);

	public int CurrentStress
	{
		get => this.currentStress.Value;
		set => this.currentStress.Value = value;
	}

	public static int MaxStress => 9;
}
