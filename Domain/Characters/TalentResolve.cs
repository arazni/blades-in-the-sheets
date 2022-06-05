namespace Domain.Characters;

public class TalentResolve : Bases.TalentAttribute
{
	public TalentAction Attune { get; private set; } = new();

	public TalentAction Command { get; private set; } = new();

	public TalentAction Consort { get; private set; } = new();

	public TalentAction Sway { get; private set; } = new();
}
