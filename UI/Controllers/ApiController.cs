using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Characters;
using Persistence.Json;

namespace UI.Controllers;

[Route("api")]
[ApiController]
public class ApiController : ControllerBase
{
	private readonly ILoader loader;

	public ApiController(ILoader loader)
	{
		this.loader = loader;
	}

	[HttpGet]
	[Route("abilities/{identifier}")]
	[ProducesResponseType(typeof(Task<IReadOnlyCollection<PlaybookSpecialAbility>>), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetAbilities(string identifier)
	{
		var abilities = await this.loader.LoadAvailableAbilitiesAsync(identifier);

		return Ok(abilities);
	}
}
