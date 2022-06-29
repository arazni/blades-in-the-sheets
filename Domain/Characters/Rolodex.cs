namespace Domain.Characters;

public class Rolodex
{
	private List<RolodexFriend> friends = new(5);

	public IReadOnlyCollection<RolodexFriend> Friends => this.friends;

	//public RolodexFriend Find(string entry) =>
	//	Friends.Single(f => f.Entry == entry);

	public RolodexFriend? CloseFriend =>
		Friends.FirstOrDefault(f => f.Closeness == FriendCloseness.CloseFriend);

	public RolodexFriend? Rival =>
		Friends.FirstOrDefault(f => f.Closeness == FriendCloseness.Rival);

	public void AssignOnlyCloseFriend(RolodexFriend closeFriend)
	{
		if (CloseFriend != null)
			CloseFriend.Closeness = FriendCloseness.Friend;

		Friends.Single(f => f == closeFriend).Closeness = FriendCloseness.CloseFriend;
	}

	public void AssignOnlyRival(RolodexFriend rival)
	{
		if (Rival != null)
			Rival.Closeness = FriendCloseness.Friend;

		Friends.Single(f => f == rival).Closeness = FriendCloseness.Rival;
	}

	public void ReplaceFriends(IReadOnlyCollection<RolodexFriend> friends) =>
		this.friends = friends.ToList();
}
