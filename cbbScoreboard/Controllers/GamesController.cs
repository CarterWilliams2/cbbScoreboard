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
    public async Task<ActionResult<List<GameDto>>> GetGames([FromQuery] string? status)
    {
        var games = await _gamesService.GetTodayGamesByStatusAsync(status);

        if (games.Count == 0)
            return NotFound("No games found for that status");

        return Ok(games);
    }

    [HttpGet("{gameId}")]
    public async Task<ActionResult<GameDto>> GetGameById(string gameId)
    {
        var game = await _gamesService.GetGameByIdAsync(gameId);

        if (game == null) 
            return NotFound($"Game with ID {gameId} not found");

        return game;        
    }
}