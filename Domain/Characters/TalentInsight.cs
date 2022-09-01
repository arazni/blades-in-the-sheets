namespace Domain.Characters;

public class TalentInsight : Bases.TalentAttribute
{
	public TalentAction Hunt { get; private set; } = new();

	public TalentAction Study { get; private set; } = new();

	public TalentAction Survey { get; private set; } = new();

	public TalentAction Tinker { get; private set; } = new();

	public override TalentAction ActionFromName(ActionName name) => name switch
	{
		ActionName.Hunt => Hunt,
		ActionName.Study => Study,
		ActionName.Survey => Survey,
		ActionName.Tinker => Tinker,
		_ => throw new NotImplementedException()
	};
}
