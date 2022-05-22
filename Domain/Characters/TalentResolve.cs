namespace Domain.Characters;

public class TalentResolve : Bases.TalentAttribute
{
	public TalentAction Attune { get; } = new();

	public TalentAction Command { get; } = new();

	public TalentAction Consort { get; } = new();

	public TalentAction Sway { get; } = new();
}
