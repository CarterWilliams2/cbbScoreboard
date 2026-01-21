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
    public async Task<ActionResult<List<GameDto>>> GetGames(
        [FromQuery] string? status,
        [FromQuery] string? conference,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50)
    {
        var games = await _gamesService.GetFilteredGamesAsync(status, conference, page, pageSize);
        return Ok(games);
    }

    [HttpGet("{gameId}")]
    public async Task<ActionResult<GameDto?>> GetGameById(string gameId)
    {
        var game = await _gamesService.GetGameByIdAsync(gameId);

        if (game == null)
            return NotFound($"Game with Id {gameId} not found");

        return game;
    }
    
}