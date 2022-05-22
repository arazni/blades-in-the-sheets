namespace Domain.Game;

// todo: edge case where calling Progress() while having Rollover could overwrite it
// when not part of the Reset() method
public class RolloverClock : ProjectClock
{
	public RolloverClock(int size) : base(size) { }

	public int Rollover { get; private set; }

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
