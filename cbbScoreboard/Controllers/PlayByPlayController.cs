using Microsoft.AspNetCore.Mvc;
using cbbScoreboard.Services;
using cbbScoreboard.Models;

namespace cbbScoreboard.Controllers;

[ApiController]
[Route("api/games")]
public class PlayByPlayController : ControllerBase
{
    private readonly PlayByPlayService _playByPlayService;

    public PlayByPlayController(PlayByPlayService playByPlayService)
    {
        _playByPlayService = playByPlayService;
    }

    
}