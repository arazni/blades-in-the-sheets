using Microsoft.AspNetCore.Components;
using Models.Characters;
using UI.Services;

namespace UI.Components.CharacterSheet;
public partial class SheetTalentAction
{
	[Parameter, EditorRequired]
	public string AttributeName { get; set; } = string.Empty;

	[Parameter, EditorRequired]
	public TalentAttribute Attribute { get; set; } = TalentAttribute.Empty();

	[Parameter, EditorRequired]
	public string ActionName { get; set; } = string.Empty;

	[Parameter, EditorRequired]
	public TalentAction Action { get; set; } = TalentAction.Empty();

	[CascadingParameter(Name = "IsFixMode")]
	public bool IsFixMode { get; set; }

	string LevelUpTitle =>
		$"Level up {ActionName}";

	bool CanLevelUp =>
		Attribute!.Experience.CanLevelUp
		|| IsFixMode;

	string Text =>
		$"{Action.Rating} {ActionName}";

	void LevelUp()
	{
		if (!Attribute.LevelUp(ActionName))
			return;

		Notify();
	}

	int FixModeRating
	{
		get => Action.Rating;
		set
		{
			Action.Rating = value;
			Notify();
		}
	}

	void Notify() =>
		SheetJank.NotifyAttributeChanged(AttributeName);

	string RatingLabel =>
		$"Set {ActionName} rating";
}