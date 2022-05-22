namespace Domain.Characters;

public class GearCommitment
{
	internal LoadCommitments Commitment { get; set; }

	public int MaxBulk => (int)Commitment;
}

public enum LoadCommitments
{
	None = 0,
	Light = 3,
	Normal = 5,
	Heavy = 6,
	Encumbered = 9
}
