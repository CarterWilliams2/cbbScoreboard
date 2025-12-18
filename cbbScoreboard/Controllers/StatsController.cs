using Microsoft.AspNetCore.Mvc;
using cbbScoreboard.Services;
using cbbScoreboard.Models;

namespace cbbScoreboard.Controllers;

[ApiController]
[Route("api/stats")]
public class StatsController: ControllerBase
{
    private readonly StatsService _statsService;

    public StatsController(GamesService statsService)
    {
        _statsService = statsService;
    }


    
}