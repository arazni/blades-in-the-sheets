namespace Models.Common;

public class Ink
{
	private string value;

	public Ink(string value)
	{
		Value = value;
		this.value = Value; // C# nullability silliness
	}

	public string Value
	{
		get => this.value;
		set
		{
			if (!value.HasInk())
				throw new ArgumentException("Ink cannot be empty or whitespace", nameof(Value));

			this.value = value.Trim();
		}
	}

	public override bool Equals(object? obj) =>
		obj is null ? false
		: obj is Ink other ? Value.Equals(other.Value)
		: false;

	public override int GetHashCode() =>
		Value.GetHashCode();
}
