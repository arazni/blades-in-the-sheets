using Domain.Common;

namespace Domain.Game;

public class ProjectClock
{
	public ProjectClock(int size)
	{
		this.clock = new(0, size);
	}

	protected readonly BoundedInteger clock;

	public int Size => this.clock.Max;

	public bool Ding => this.clock.Value == this.clock.Max;

	public int Time => this.clock.Value;

	public virtual void Progress(int segments)
	{
		this.clock.ChangeUntilBound(segments);
	}

	public virtual void Reset()
	{
		this.clock.Value = 0;
	}
}
