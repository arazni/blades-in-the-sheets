using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Models.Characters;
using Models.Settings;

namespace UI.Components.CharacterSheet;
public partial class SheetRolodexCard
{
	[Parameter, EditorRequired]
	public Rolodex Rolodex { get; set; } = new();

	[Parameter, EditorRequired]
	public RolodexSetting RolodexSetting { get; set; } = EmptyGameSetting.Rolodex();

	bool IsFixMode { get; set; } = false;

	string AddableContactName = string.Empty;

	static readonly Icon closeFriendIcon = new Icons.Filled.Size20.Heart();

	static readonly Icon rivalIcon = new Icons.Filled.Size20.Warning();

	static readonly Icon contactIcon = new Icons.Filled.Size20.Chat();

	static Icon GetFriendIcon(RolodexFriend friend) => friend.Closeness switch
	{
		FriendCloseness.CloseFriend => closeFriendIcon,
		FriendCloseness.Rival => rivalIcon,
		_ => contactIcon
	};

	//static Color GetFriendIconColor(RolodexFriend friend) => friend.Closeness switch
	//{
	//	FriendCloseness.CloseFriend => Color.Secondary,
	//	FriendCloseness.Rival => Color.Warning,
	//	_ => Color.Default
	//};

	static string GetAdornmentLabel(RolodexFriend friend) => friend.Closeness switch
	{
		FriendCloseness.CloseFriend => "Close friend",
		FriendCloseness.Rival => "Rival",
		_ => "Friend"
	};

	static string ButtonLabel(RolodexFriend friend, string action) =>
		$"{action} {friend.Entry}";

	void AddFriend()
	{
		Rolodex.AddFriend(new(AddableContactName));
		AddableContactName = string.Empty;
	}
}