namespace Models.Settings;

public class ThemeSetting
{
	public required string NeutralBaseColor { get; set; }

	public required string AccentBaseColor { get; set; }

	public required float BaseLayerLuminence { get; set; }

	public required string ScareColor { get; set; }
}