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

    [HttpGet("{stat}")]
    public async Task<ActionResult<List<StatDto>>> GetStats(string stat)
    {
        var stats = await _statsService.GetStatsAsync(stat);
        return Ok(stats);
    }
    
}