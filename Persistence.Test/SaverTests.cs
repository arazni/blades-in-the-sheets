using Domain.Characters;
using Persistence.Json;
using System.Threading.Tasks;
using Xunit;

namespace Persistence.Test;

public class SaverTests
{
	private readonly ISaver saver;

	public SaverTests()
	{
		this.saver = new Saver();
	}

	[Fact]
	public async Task SavesEmptyCharacter()
	{
		await this.saver.Save(new Character(PlaybookOption.Unknown));
	}
}
