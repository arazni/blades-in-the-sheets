namespace Domain.Characters;

public class Rolodex
{
	private readonly List<RolodexFriend> friends = new(5);

	public IReadOnlyCollection<RolodexFriend> Friends => this.friends;

	public RolodexFriend Find(string entry) =>
		Friends.Single(f => f.Entry == entry);
}
