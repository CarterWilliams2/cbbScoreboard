namespace cbbScoreboard.Controllers;

[ApiController]
[Route("api/win-probability")]
public class WinProbabilityController: ControllerBase
{
    private readonly WinProbabilityService _winProbabilityService;

    public GamesController(WinProbabilityService winProbabilityService)
    {
        _winProbabilityService = winProbabilityService;
    }

    [HttpGet]
    public async Task<ActionResult<List<GameDto>>> GetGames(
        [FromQuery] string? status,
        [FromQuery] string? conference,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var games = await _gamesService.GetFilteredGamesAsync(status, conference, page, pageSize);
        return Ok(games);
    }
    
}