namespace Models.Characters;

public class GearCommitment
{
	public LoadCommitmentOption Commitment { get; set; }

	public int MaxBulk => (int)Commitment;
}

public enum LoadCommitmentOption
{
	None = 0,
	Light = 3,
	Normal = 5,
	Heavy = 6,
	Encumbered = 9
}
