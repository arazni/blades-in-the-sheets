namespace Models.Common;

public class CInk : Ink
{
	public CInk(string value) : base(value) { }

	public override bool Equals(object? obj) =>
		obj is null ? false
		: obj is CInk other ? Value.Like(other.Value)
		: false;

	public override int GetHashCode() =>
		Value.ToLowerInvariant().GetHashCode();
}
