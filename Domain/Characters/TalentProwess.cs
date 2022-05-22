namespace Domain.Characters;

public class TalentProwess : Bases.TalentAttribute
{
	public TalentAction Finesse { get; } = new();

	public TalentAction Prowl { get; } = new();

	public TalentAction Skirmish { get; } = new();

	public TalentAction Wreck { get; } = new();
}
