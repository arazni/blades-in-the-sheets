namespace Models.Settings;
public class AccessibilitySetting
{
	public RatingImplementation Rating { get; set; }

	public enum RatingImplementation
	{
		Checkboxes,
		Slider
	}

	public void SetRating(bool useSlider)
	{
		Rating = useSlider ? RatingImplementation.Slider
		: RatingImplementation.Checkboxes;
	}
}
