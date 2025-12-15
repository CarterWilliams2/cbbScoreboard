using Microsoft.AspNetCore.Mvc;
using cbbScoreboard.Services;
using cbbScoreboard.Models;

namespace cbbScoreboard.Controllers;

[ApiController]
[Route("api/games")]
public class GamesController: ControllerBase
{
    private readonly GamesService _gamesService;

    public GamesController(GamesService gamesService)
    {
        _gamesService = gamesService;
    }

    [HttpGet]
    public async Task<ActionResult<List<GameDto>>> GetGames()
    {
        var games = await _gamesService.GetTodayGamesAsync();
        return Ok(games);
    }
}