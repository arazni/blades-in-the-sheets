namespace Domain.Characters;

public class TalentProwess : Bases.TalentAttribute
{
	public TalentAction Finesse { get; private set; } = new();

	public TalentAction Prowl { get; private set; } = new();

	public TalentAction Skirmish { get; private set; } = new();

	public TalentAction Wreck { get; private set; } = new();

	public override TalentAction ActionFromName(ActionName name) => name switch
	{
		ActionName.Finesse => Finesse,
		ActionName.Prowl => Prowl,
		ActionName.Skirmish => Skirmish,
		ActionName.Wreck => Wreck,
		_ => throw new NotImplementedException()
	};
}
