using Microsoft.AspNetCore.Mvc;
using cbbScoreboard.Services;
using cbbScoreboard.Models;

namespace cbbScoreboard.Controllers;

[ApiController]
[Route("api/play-by-play")]
public class PlayByPlayController : ControllerBase
{
    private readonly PlayByPlayService _playByPlayService;

    public PlayByPlayController(PlayByPlayService playByPlayService)
    {
        _playByPlayService = playByPlayService;
    }

    [HttpGet("{gameId}")]
    public async Task<ActionResult<List<PlayByPlayDto>>> GetPlays(string gameId)
    {
        var plays = await _playByPlayService.GetPlayByPlayAsync(gameId);
        return Ok(plays);
    }
}