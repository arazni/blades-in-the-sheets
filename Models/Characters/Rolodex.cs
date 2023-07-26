namespace Models.Characters;

public class Rolodex
{
	private List<RolodexFriend> friends = new(5);

	public IReadOnlyCollection<RolodexFriend> Friends
	{
		get => this.friends;
		private set => ReplaceFriends(value);
	}

	public IReadOnlyCollection<RolodexFriend> CloseFriends =>
		Friends.Where(f => f.Closeness == FriendCloseness.CloseFriend)
			.ToList();

	public IReadOnlyCollection<RolodexFriend> Rivals =>
		Friends.Where(f => f.Closeness == FriendCloseness.Rival)
			.ToList();

	public void UpgradeFriend(RolodexFriend friend) =>
		Friends.Single(f => f == friend).Closeness = FriendCloseness.CloseFriend;

	public void DowngradeFriend(RolodexFriend friend) =>
		Friends.Single(f => f == friend).Closeness = FriendCloseness.Rival;

	public void DowngradeCloseFriend(RolodexFriend closeFriend) =>
		Friends.Single(f => f == closeFriend).Closeness = FriendCloseness.Friend;

	public void UpgradeRival(RolodexFriend rival) =>
		Friends.Single(f => f == rival).Closeness = FriendCloseness.Friend;

	public void ReplaceFriends(IReadOnlyCollection<RolodexFriend> friends) =>
		this.friends = friends.ToList();

	public void ReplaceFriends(RolodexCreation creation) =>
		ReplaceFriends(creation.Friends);

	public bool HasCloseFriends =>
		CloseFriends.Any();

	public bool HasRivals =>
		Rivals.Any();
}
