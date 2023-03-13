namespace Models.Game;

// todo: edge case where calling Progress() while having Rollover could overwrite it
// when not part of the Reset() method
public class RolloverClock : ProjectClock
{
	public RolloverClock(int size, int time = 0, int rollover = 0) : base(size)
	{
		Time = time;
		Rollover = rollover;
	}

	public int Rollover { get; private set; }

	public override int Time
	{
		get => base.Time;
		set
		{
			base.Time = value;
			Rollover = 0; // hacky?
		}
	}

	public override void Progress(int segments)
	{
		Rollover = this.clock.ChangeUntilBound(segments);
	}

	public override void Reset()
	{
		base.Reset();
		Progress(Rollover);
	}
}
