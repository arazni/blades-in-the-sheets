using Microsoft.AspNetCore.Components;
using Models.Characters;
using UI.Services;

namespace UI.Components.CharacterSheet;
public sealed partial class SheetTalentAttribute
{
	[Parameter, EditorRequired]
	public TalentAttribute Attribute { get; set; } = TalentAttribute.Empty();

	[Parameter, EditorRequired]
	public string Name { get; set; } = string.Empty;

	[CascadingParameter(Name = "IsFixMode")]
	public bool IsFixMode { get; set; }

	protected override void OnInitialized()
	{
		SheetJank.AttributeChanged += AttributeHasChanged;

		base.OnInitialized();
	}

	public void Dispose()
	{
		SheetJank.AttributeChanged -= AttributeHasChanged;
	}

	private void AttributeHasChanged(string attributeName)
	{
		if (attributeName == Name)
			StateHasChanged();
	}

	string RatingLabel =>
		$"{Name} experience points";
}