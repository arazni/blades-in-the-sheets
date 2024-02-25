using Microsoft.AspNetCore.Components;
using static UI.Constants;

namespace UI.Components.NewCharacter;

public partial class NewCharacterPlaybookOption
{
	[Parameter, EditorRequired]
	public string PlaybookName { get; set; } = string.Empty;

	[Parameter, EditorRequired]
	public string GameFileStem { get; set; } = string.Empty;

	[Parameter, EditorRequired]
	public string Hook { get; set; } = string.Empty;

	string Path => Paths.NewCharacter(GameFileStem, PlaybookName);
}
