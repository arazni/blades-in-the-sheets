using Microsoft.AspNetCore.Components;
using Models.Characters;

namespace UI.Components.CharacterSheet;
public partial class SheetTalentCard
{
	[Parameter, EditorRequired]
	public Talent Talent { get; set; } = Talent.Empty();

	public bool IsFixMode { get; set; } = false;
}