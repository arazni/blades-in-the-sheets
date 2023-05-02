﻿using Models.Settings;

namespace Models.Characters;

public class Talent
{
	private Dictionary<string, TalentAttribute> attributesByName = new();

	private Talent() { }

	public Talent(AttributeSetting[] attributes, DefaultActionPointSetting[] defaultPoints, int actionPointMaximum)
	{
		AttributesByName = attributes.ToDictionary
		(
			setting => setting.Name,
			setting => new TalentAttribute(setting, actionPointMaximum),
			StringComparer.OrdinalIgnoreCase
		);

		if (!AssignDefaultPoints(defaultPoints))
			throw new ArgumentOutOfRangeException(nameof(defaultPoints));
	}

	public IReadOnlyDictionary<string, TalentAttribute> AttributesByName
	{
		get => this.attributesByName;
		private set => this.attributesByName = value.ToDictionary
		(
			k => k.Key,
			v => v.Value,
			StringComparer.OrdinalIgnoreCase
		);
	}

	public IReadOnlyDictionary<string, TalentAction> ActionsByName =>
		AttributesByName.Values
			.SelectMany(a => a.ActionsByName)
			.ToDictionary
			(
				actionByName => actionByName.Key,
				actionByName => actionByName.Value,
				StringComparer.OrdinalIgnoreCase
			);

	private bool AssignDefaultPoints(DefaultActionPointSetting[] settings)
	{
		foreach (var setting in settings)
		{
			if (!ActionsByName.TryGetValue(setting.Action, out var action))
				return false;

			action.Rating = setting.Points;
		}

		return true;
	}

	public static Talent Empty() => new();
}
