namespace Domain.Characters;

public class TalentProwess : Bases.TalentAttribute
{
	public TalentAction Finesse { get; private set; } = new();

	public TalentAction Prowl { get; private set; } = new();

	public TalentAction Skirmish { get; private set; } = new();

	public TalentAction Wreck { get; private set; } = new();
}
