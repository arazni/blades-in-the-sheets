namespace Domain.Characters;

public class Rolodex
{
	private readonly RolodexFriend[] friends;

	public Rolodex(RolodexFriend[] friends)
	{
		this.friends = friends;
	}

	public IReadOnlyCollection<RolodexFriend> Friends => this.friends;

	public RolodexFriend Find(string entry) =>
		Friends.Single(f => f.Entry == entry);
}
