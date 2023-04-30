using FluentAssertions;
using Models.Settings;
using Persistence.Json;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static Models.Legacy.RetiredOptions;

namespace Persistence.Test;
public class LoaderTests
{
	private readonly ILoader loader;
	private readonly IFileReader reader;
	public LoaderTests()
	{
		this.reader = new ServerFileReader();
		this.loader = new Loader(this.reader);
	}

	[Theory]
	[InlineData(PlaybookOption.Cutter)]
	[InlineData(PlaybookOption.Hound)]
	[InlineData(PlaybookOption.Leech)]
	[InlineData(PlaybookOption.Lurk)]
	[InlineData(PlaybookOption.Slide)]
	[InlineData(PlaybookOption.Spider)]
	[InlineData(PlaybookOption.Whisper)]
	public async Task LoadsAvailableFriends(PlaybookOption playbook)
	{
		var setting = await this.loader.LoadSettings(Constants.Games.BladesInTheDark);

		var result = setting.GetPlaybookSetting(playbook.ToString())
			.Rolodex;

		result.Should().NotBeNull();
		result.Name.Should().NotBeNullOrWhiteSpace();
		result.Friends.Should().NotBeEmpty();
	}

	[Theory]
	[InlineData(PlaybookOption.Cutter)]
	[InlineData(PlaybookOption.Hound)]
	[InlineData(PlaybookOption.Leech)]
	[InlineData(PlaybookOption.Lurk)]
	[InlineData(PlaybookOption.Slide)]
	[InlineData(PlaybookOption.Spider)]
	[InlineData(PlaybookOption.Whisper)]
	public async Task LoadsAvailableAbilities(PlaybookOption playbook)
	{
		var setting = await this.loader.LoadSettings(Constants.Games.BladesInTheDark);

		var result = setting.GetPlaybookSetting(playbook.ToString())
			.SpecialAbilities;

		result.Should().NotBeEmpty();
		result.Should().HaveCountGreaterThanOrEqualTo(8);
		result.Sum(r => r.TimesTakeable).Should().Be(12);
	}
}