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
	public LoaderTests()
	{
		this.loader = new Loader();
	}

	[Theory]
	[InlineData(Playbooks.Cutter)]
	[InlineData(Playbooks.Hound)]
	[InlineData(Playbooks.Leech)]
	[InlineData(Playbooks.Lurk)]
	[InlineData(Playbooks.Slide)]
	[InlineData(Playbooks.Spider)]
	[InlineData(Playbooks.Whisper)]
	public async Task LoadsAvailableFriends(Playbooks playbook)
	{
		var result = await this.loader.LoadAvailableFriendsAsync(playbook.ToString());

		result.Should().NotBeEmpty();
	}

	[Theory]
	[InlineData(Playbooks.Cutter)]
	[InlineData(Playbooks.Hound)]
	[InlineData(Playbooks.Leech)]
	[InlineData(Playbooks.Lurk)]
	[InlineData(Playbooks.Slide)]
	[InlineData(Playbooks.Spider)]
	[InlineData(Playbooks.Whisper)]
	public async Task LoadsAvailableAbilities(Playbooks playbook)
	{
		var result = await this.loader.LoadAvailableAbilitiesAsync(playbook.ToString());

		result.Should().NotBeEmpty();
		result.Should().HaveCountGreaterThanOrEqualTo(8);
		result.Sum(r => r.TimesTakable).Should().Be(12);
	}

	[Theory]
	[InlineData(Playbooks.Cutter)]
	[InlineData(Playbooks.Hound)]
	[InlineData(Playbooks.Leech)]
	[InlineData(Playbooks.Lurk)]
	[InlineData(Playbooks.Slide)]
	[InlineData(Playbooks.Spider)]
	[InlineData(Playbooks.Whisper)]
	public async Task LoadsAvailableItems(Playbooks playbook)
	{
		var result = await this.loader.LoadAvailableItemsAsync(playbook.ToString());

		result.Should().NotBeEmpty();
		result.Should().HaveCountGreaterThanOrEqualTo(6);
		result.Select(r => r.Bulk).Should().Contain(0);
		result.Select(r => r.Name).Should().OnlyHaveUniqueItems();
	}
}