using Microsoft.AspNetCore.Components;
using Models.Characters;

namespace UI.Components.NewCharacter;

public partial class NewCharacterIdentificationCard
{
	[Parameter, EditorRequired]
	public Dossier Dossier { get; set; } = new();
}
