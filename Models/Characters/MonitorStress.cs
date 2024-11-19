using Models.Common;

namespace Models.Characters;

public class MonitorStress
{
	private readonly BoundedInteger currentStress = new(DefaultMaxStress);

	public int CurrentStress
	{
		get => this.currentStress.Value;
		set => this.currentStress.Value = value;
	}

	public int MaxStress
	{
		get => this.currentStress.Max;
		set => this.currentStress.Max = value;
	}

	public const int DefaultMaxStress = 9;
}
