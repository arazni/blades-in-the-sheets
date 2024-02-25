using Models.Settings;

namespace UI.Pages;
public partial class Accessibility
{
	bool UseSlider { get; set; }

	int RatingSwitchValue { get; set; } = 1;

	AccessibilitySetting accessibilitySetting = new();

	protected override async Task OnInitializedAsync()
	{
		this.accessibilitySetting.Rating = await SettingService.GetRatingImplementation();
		UseSlider = this.accessibilitySetting.Rating == AccessibilitySetting.RatingImplementation.Slider;

		await base.OnInitializedAsync();
	}

	async Task RatingSwitchValueChanged(bool useSlider)
	{
		this.accessibilitySetting.SetRating(useSlider);
		UseSlider = useSlider;
		await SettingService.SetAccessibilitySetting(this.accessibilitySetting);
	}
}