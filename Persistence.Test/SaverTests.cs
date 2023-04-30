using Models.Characters;
using Models.Settings;
using Persistence.Json;
using System.Threading.Tasks;
using Xunit;
using static Models.Legacy.RetiredOptions;

namespace Persistence.Test;

public class SaverTests
{
	private readonly ISaver saver;
	private readonly ILoader loader;

	public SaverTests()
	{
		this.saver = new Saver();
		this.loader = new Loader(new ServerFileReader());
	}

	[Fact]
	public async Task SavesEmptyCharacter()
	{
		var gameSetting = await this.loader.LoadSettings(Constants.Games.BladesInTheDark);
		await this.saver.Save(new Character(gameSetting, PlaybookOption.Lurk.ToString()));
	}
}
