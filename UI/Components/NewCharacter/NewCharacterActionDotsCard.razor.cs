using Microsoft.AspNetCore.Components;
using Models.Characters;
using Models.Settings;
using UI.ViewModels;

namespace UI.Components.NewCharacter;
public partial class NewCharacterActionDotsCard
{
	[Parameter, EditorRequired]
	public Character Character { get; set; } = Character.Empty();

	[Parameter, EditorRequired]
	public GameSetting GameSetting { get; set; } = EmptyGameSetting.Game();

	private V4ActionDots ViewModel { get; set; } = V4ActionDots.Empty();

	protected override void OnParametersSet()
	{
		ViewModel = new(Character, GameSetting, Character.Playbook.Name);

		base.OnParametersSet();
	}

	static string DotLabel(string actionName, int dot, bool isFilled) =>
		$"{actionName} dot {dot} {(isFilled ? "filled" : "unfilled")}";
}