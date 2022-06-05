using Domain.Characters;
using FluentAssertions;
using Persistence.Json;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

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
		var result = await this.loader.LoadAvailableFriendsAsync(playbook.ToString());

		result.Should().NotBeEmpty();
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
		var result = await this.loader.LoadAvailableAbilitiesAsync(playbook.ToString());

		result.Should().NotBeEmpty();
		result.Should().HaveCountGreaterThanOrEqualTo(8);
		result.Sum(r => r.TimesTakable).Should().Be(12);
	}

	[Theory]
	[InlineData(PlaybookOption.Cutter)]
	[InlineData(PlaybookOption.Hound)]
	[InlineData(PlaybookOption.Leech)]
	[InlineData(PlaybookOption.Lurk)]
	[InlineData(PlaybookOption.Slide)]
	[InlineData(PlaybookOption.Spider)]
	[InlineData(PlaybookOption.Whisper)]
	public async Task LoadsAvailableItems(PlaybookOption playbook)
	{
		var result = await this.loader.LoadAvailableItemsAsync(playbook.ToString());

		result.Should().NotBeEmpty();
		result.Should().HaveCountGreaterThanOrEqualTo(6);
		result.Select(r => r.Bulk).Should().Contain(0);
		result.Select(r => r.Name).Should().OnlyHaveUniqueItems();
	}
}