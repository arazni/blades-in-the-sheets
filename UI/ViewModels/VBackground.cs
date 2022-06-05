using Domain.Characters;

namespace UI.ViewModels;

public class VBackground
{
	public string DisplayName => Option.ToString();

	public string Example { get; set; } = string.Empty;

	public BackgroundOption Option { get; set; } = BackgroundOption.Unknown;

	public static IReadOnlyCollection<VBackground> Options { get; } = new VBackground[]
	{
		new VBackground
		{
			Option = BackgroundOption.Unknown,
			Example = "Choose a background to learn more about it.",
		},
		new VBackground
		{
			Option = BackgroundOption.Academic,
			Example = "A scholar, a professor or student from Doskvol Academy, a philosopher or journalist, etc."
		},
		new VBackground
		{
			Option = BackgroundOption.Labor,
			Example = "A servant, a factory worker, a coach driver, a docker, a sailor, a Rail Jack, etc."
		},
		new VBackground
		{
			Option = BackgroundOption.Law,
			Example = "An advocate or barrister, a Bluecoat or inspector (or even Spirit Warden), a prison guard from Ironhook, etc."
		},
		new VBackground
		{
			Option = BackgroundOption.Trade,
			Example = "A shopkeeper, a merchant, a skilled crafts-person, a shipping agent, etc."
		},
		new VBackground
		{
			Option = BackgroundOption.Military,
			Example = "An Imperial or Skovlander soldier, a mercenary, an intelligence operative, a strategist, a training instructor, etc."
		},
		new VBackground
		{
			Option = BackgroundOption.Noble,
			Example = "A dilettante, a courtier, the scion of a fallen house, etc."
		},
		new VBackground
		{
			Option = BackgroundOption.Underworld,
			Example = "A street urchin, gang member, young thug, or other outcast who grew up on the streets."
		}
	};
}
