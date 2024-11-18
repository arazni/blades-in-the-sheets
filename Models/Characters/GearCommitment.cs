using System.Collections.Immutable;

namespace Models.Characters;

public class GearCommitment
{
	public IDictionary<LoadCommitmentOption, int> MaxBulkByCommitmentOption { get; private set; } = AllLoadCommitmentOptions.ToDictionary
	(
		load => load,
		load => (int)load
	);

	public void SetMaxBulkForCommitmentOption(LoadCommitmentOption load, int bulk) =>
		MaxBulkByCommitmentOption[load] = bulk;

	public LoadCommitmentOption Commitment { get; set; }

	public int MaxBulk => MaxBulkByCommitmentOption[Commitment];

	public static readonly ImmutableArray<LoadCommitmentOption> AllLoadCommitmentOptions =
	[
		LoadCommitmentOption.None,
		LoadCommitmentOption.Light,
		LoadCommitmentOption.Normal,
		LoadCommitmentOption.Heavy,
		LoadCommitmentOption.Encumbered
	];

	public static readonly ImmutableArray<LoadCommitmentOption> EditableLoadCommitmentOptions =
		AllLoadCommitmentOptions
		.Skip(1)
		.ToImmutableArray();
}

public enum LoadCommitmentOption
{
	None = 0,
	Light = 3,
	Normal = 5,
	Heavy = 6,
	Encumbered = 9
}
