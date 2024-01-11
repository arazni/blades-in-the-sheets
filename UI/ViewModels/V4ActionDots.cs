using Models.Characters;
using Models.Settings;

namespace UI.ViewModels;

public class V4ActionDots
{
	private readonly DossierBackground background;
	private readonly DossierHeritage heritage;

	private V4ActionDots()
	{
		this.background = new();
		this.heritage = new();
		Attributes = Array.Empty<VTalentAttribute>();
	}

	public V4ActionDots(Character character, GameSetting gameSetting, string playbookName)
	{
		this.background = character.Dossier.Background;
		this.heritage = character.Dossier.Heritage;

		Attributes = character.Talent.AttributesByName.Select(attributeByName =>
			new VTalentAttribute
			(
				attributeByName.Value,
				attributeByName.Key,
				attributeByName.Value.ActionsByName.Select(actionByName =>
					new VTalentAction
					(
						actionByName.Value,
						gameSetting,
						attributeByName.Key,
						actionByName.Key,
						playbookName
					)
				).ToArray()
			)
		).ToArray();
	}

	public const int FinalDots = 7;

	public int TotalDots =>
		Attributes.SelectMany(a => a.Actions)
			.Sum(a => a.Action.Rating);

	public int RemainingDots => FinalDots - TotalDots;

	public string Background => this.background.Name;

	public string BackgroundDescription => this.background.Description;

	public string Heritage => this.heritage.Name;

	public string HeritageDescription => this.heritage.Description;

	public bool IsDotOneEnabled(VTalentAction action) =>
		!action.IsDotOneDefaulted && (action.IsDotOneFilled || RemainingDots > 0);

	public bool IsDotTwoEnabled(VTalentAction action) =>
		!action.IsDotTwoDefaulted && (action.IsDotTwoFilled || (action.IsDotOneFilled && RemainingDots > 0) || (RemainingDots > 1));

	public IReadOnlyCollection<VTalentAttribute> Attributes { get; }

	public static V4ActionDots Empty() => new();
}

public class VTalentAttribute(TalentAttribute attribute, string name, IReadOnlyCollection<VTalentAction> actions)
{
	public TalentAttribute Attribute { get; } = attribute;

	public string Name { get; } = name;

	public IReadOnlyCollection<VTalentAction> Actions { get; } = actions;
}

public class VTalentAction
{
	private readonly int actionDefaultPoints = 0;

	public VTalentAction(TalentAction action, GameSetting gameSetting, string attributeName, string actionName, string playbookName)
	{
		Action = action;
		var actionSetting = gameSetting.GetActionSetting(attributeName, actionName);
		Name = actionSetting.Name;
		ShortDescription = actionSetting.ShortDescription;
		LongDescription = actionSetting.LongDescription;
		this.actionDefaultPoints = gameSetting.DefaultActionPoints(playbookName, actionName);
	}

	public TalentAction Action { get; }

	public bool IsDotOneDefaulted => this.actionDefaultPoints > 0;

	public bool IsDotTwoDefaulted => this.actionDefaultPoints > 1;

	public bool IsDotOneFilled => Action.Rating > 0;

	public bool IsDotTwoFilled => Action.Rating > 1;

	public void ToggleDotOne(bool toggledOn)
	{
		if (IsDotOneDefaulted)
			return;

		if (!toggledOn)
			Action.Rating = 0;
		else
			Action.Rating = 1;
	}

	public void ToggleDotTwo(bool toggledOn)
	{
		if (IsDotTwoDefaulted)
			return;

		if (!toggledOn)
			Action.Rating = 1;
		else
			Action.Rating = 2;
	}

	public string Name { get; }

	public string ShortDescription { get; }

	public string LongDescription { get; }
}