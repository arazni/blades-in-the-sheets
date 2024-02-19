//using Microsoft.AspNetCore.Components;
//using Microsoft.FluentUI.AspNetCore.Components;

//namespace UI.Shared;
//public partial class FluentToggleButton
//{
//	[EditorRequired, Parameter]
//	public string ToggleOnTitle { get; set; } = string.Empty;

//	[EditorRequired, Parameter]
//	public string ToggleOffTitle { get; set; } = string.Empty;

//	[EditorRequired, Parameter]
//	public Icon? ToggleOnIcon { get; set; }

//	[EditorRequired, Parameter]
//	public Icon? ToggleOffIcon { get; set; }

//	[EditorRequired, Parameter]
//	public virtual bool IsToggledOn { get; set; } = false;

//	[Parameter]
//	public virtual EventCallback<bool> IsToggledOnChanged { get; set; }

//	string ToggledOnClass => UI.Constants.Classes.Display(IsToggledOn);

//	string ToggledOffClass => UI.Constants.Classes.DisplayNone(IsToggledOn);
//}