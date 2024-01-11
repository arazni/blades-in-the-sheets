﻿using Microsoft.AspNetCore.Components;
using Models.Characters;
using Models.Settings;

namespace UI.Components.NewCharacter;

public partial class NewCharacterBackgroundCard
{
	[Parameter, EditorRequired]
	public DossierBackground Background { get; set; } = new();

	[Parameter, EditorRequired]
	public BackgroundSetting[] BackgroundSettings { get; set; } = EmptyGameSetting.Backgrounds();

	string Example() =>
		BackgroundSettings.FirstOrDefault(b => b.Name == Background.Name)?.Example
		?? string.Empty;
}
