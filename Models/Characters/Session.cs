using Models.Common;

namespace Models.Characters;
public class Session
{
	public const int MaxExpressions = 2;

	private readonly BoundedInteger playbookExpression = new(MaxExpressions);
	private readonly BoundedInteger characterExpression = new(MaxExpressions);
	private readonly BoundedInteger struggleExpression = new(MaxExpressions);

	public int PlaybookExpressions
	{
		get => this.playbookExpression.Value;
		set => this.playbookExpression.Value = value;
	}

	public int CharacterExpressions
	{
		get => this.characterExpression.Value;
		set => this.characterExpression.Value = value;
	}

	public int StruggleExpressions
	{
		get => this.struggleExpression.Value;
		set => this.struggleExpression.Value = value;
	}
}
