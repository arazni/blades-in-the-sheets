﻿using Models.Common;
using System.ComponentModel;

namespace Models.Characters;

public class RolodexFriend(string entry)
{
	private readonly Ink entry = new(entry);

	public string Entry
	{
		get => this.entry.Value;
		set => this.entry.Value = value;
	}

	public FriendCloseness Closeness { get; internal set; }
}

public enum FriendCloseness
{
	Friend = default,
	Rival,
	[Description("Close friend")]
	CloseFriend
}