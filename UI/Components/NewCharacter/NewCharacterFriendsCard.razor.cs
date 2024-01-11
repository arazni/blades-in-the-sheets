using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Models.Characters;
using Models.Common;
using Models.Settings;

namespace UI.Components.NewCharacter;
public partial class NewCharacterFriendsCard
{
	[Parameter, EditorRequired]
	public RolodexCreation Rolodex { get; set; } = new();

	[Parameter, EditorRequired]
	public string PlaybookName { get; set; } = string.Empty;

	[Parameter, EditorRequired]
	public RolodexSetting RolodexSetting { get; set; } = EmptyGameSetting.Rolodex();

	private RolodexFriend? CloseFriend { get; set; }

	private RolodexFriend? Rival { get; set; }

	private Option<RolodexFriend>[] AvailableFriendOptions { get; set; } = [];

	private RolodexFriend[] RemainingFriends =>
		Rolodex.Friends.Where(f => !f.Closeness.In(FriendCloseness.Rival, FriendCloseness.CloseFriend))
			.ToArray();

	protected override void OnParametersSet()
	{
		if (!PlaybookName.HasInk())
			return;

		if (Rolodex.Friends.Any())
		{
			CloseFriend = Rolodex.CloseFriend;
			Rival = Rolodex.Rival;
			return;
		}

		var friends = RolodexSetting.Friends.Select(f => new RolodexFriend(f))
			.ToArray();

		Rolodex.ReplaceFriends(friends);

		AvailableFriendOptions = Rolodex.Friends
			.Select(f => new Option<RolodexFriend> { Value = f, Text = f })
			.Prepend(new Option<RolodexFriend> { Value = null, Text = null, Disabled = true, Selected = true })
			.ToArray();

		base.OnParametersSet();
	}

	private void OnCloseFriendChanged(Option<RolodexFriend> friend)
	{
		if (friend.Value == null)
		{
			Rolodex.RemoveCloseFriends();
			return;
		}

		Rolodex.AssignOnlyCloseFriend(friend.Value);
		CloseFriend = Rolodex.CloseFriend;
	}

	private void OnRivalChanged(Option<RolodexFriend> rival)
	{
		if (rival.Value == null)
		{
			Rolodex.RemoveRivals();
			return;
		}

		Rolodex.AssignOnlyRival(rival.Value);
		Rival = Rolodex.Rival;
	}
}