namespace Domain.Characters;

public class TalentInsight : Bases.TalentAttribute
{
	public TalentAction Hunt { get; private set; } = new();

	public TalentAction Study { get; private set; } = new();

	public TalentAction Survey { get; private set; } = new();

	public TalentAction Tinker { get; private set; } = new();
}
