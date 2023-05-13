using System.ComponentModel;

namespace Models.Legacy;

// May be necessary to help with data migrations
public static class RetiredOptions
{
	public enum TraumaOption
	{
		Cold,
		Haunted,
		Obsessed,
		Paranoid,
		Reckless,
		Soft,
		Unstable,
		Vicious
	}

	public enum HeritageOption
	{
		Unknown = 0,
		Akoros,
		[Description("The Dagger Isles")]
		TheDaggerIsles,
		Iruvia,
		Severos,
		Skovlan,
		Tycheros
	}

	public enum ViceOption
	{
		Unknown = 0,
		Faith,
		Gambling,
		Luxury,
		Obligation,
		Pleasure,
		Stupor,
		Weird
	}

	public enum BackgroundOption
	{
		Unknown = 0,
		Academic,
		Labor,
		Law,
		Trade,
		Military,
		Noble,
		Underworld
	}

	public enum AttributeName
	{
		Insight,
		Prowess,
		Resolve
	}

	public enum ActionName
	{
		// Insight
		Hunt,
		Study,
		Survey,
		Tinker,
		// Prowess
		Finesse,
		Prowl,
		Skirmish,
		Wreck,
		// Resolve
		Attune,
		Command,
		Consort,
		Sway
	}

	public enum PlaybookOption
	{
		Unknown = 0,
		Cutter,
		Hound,
		Leech,
		Lurk,
		Slide,
		Spider,
		Whisper
	}

	public enum SourceOption
	{
		Standard,
		Cutter,
		Hound,
		Leech,
		Lurk,
		Slide,
		Spider,
		Whisper,
		Custom
	}
}
