namespace Models.Characters;

public class TalentResolve : Bases.TalentAttribute
{
	public TalentAction Attune { get; private set; } = new();

	public TalentAction Command { get; private set; } = new();

	public TalentAction Consort { get; private set; } = new();

	public TalentAction Sway { get; private set; } = new();

	public override TalentAction ActionFromName(ActionName name) => name switch
	{
		ActionName.Attune => Attune,
		ActionName.Command => Command,
		ActionName.Consort => Consort,
		ActionName.Sway => Sway,
		_ => throw new NotImplementedException()
	};
}
