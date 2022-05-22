namespace Domain.Characters;

public class TalentInsight : Bases.TalentAttribute
{
	public TalentAction Hunt { get; } = new();

	public TalentAction Study { get; } = new();

	public TalentAction Survey { get; } = new();

	public TalentAction Tinker { get; } = new();
}
