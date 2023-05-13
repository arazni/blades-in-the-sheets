using Models.Common;

namespace Models.Game;

public class ProjectClock
{
	public ProjectClock(int size)
	{
		this.clock = new(size);
	}

	protected readonly BoundedInteger clock;

	public int Size => this.clock.Max;

	public bool Ding => this.clock.Value == this.clock.Max;

	public virtual int Time
	{
		get => this.clock.Value;
		set => this.clock.Value = value;
	}

	public virtual void Progress(int segments)
	{
		this.clock.ChangeUntilBound(segments);
	}

	public virtual void Reset()
	{
		this.clock.Value = 0;
	}
}
