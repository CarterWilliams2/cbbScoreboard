using Microsoft.AspNetCore.Mvc;
using cbbScoreboard.Services;
using cbbScoreboard.Models;

namespace cbbScoreboard.Controllers;

[ApiController]
[Route("api/stats")]
public class StatsController: ControllerBase
{
    private readonly StatsService _statsService;

    public StatsController(StatsService statsService)
    {
        _statsService = statsService;
    }

    [HttpGet]
    public async Task<ActionResult<List<StatDto>>> GetStats(
        [FromQuery] string? path,
        [FromQuery] string? statName
    )
    {
        var stats = await _statsService.GetStatsAsync(path, statName);
        return Ok(stats);
    }
    
}