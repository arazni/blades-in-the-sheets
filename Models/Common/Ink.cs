namespace Models.Common;

public class Ink
{
	private string value;

	public Ink(string value)
	{
		if (string.IsNullOrWhiteSpace(value))
			throw new ArgumentException(value, nameof(value));

		this.value = value.Trim();
	}

	public string Value 
	{
		get => this.value;
		set
		{
			if (string.IsNullOrWhiteSpace(value))
				throw new ArgumentException(Value, nameof(Value));

			this.value = value.Trim();
		}
	}
}
