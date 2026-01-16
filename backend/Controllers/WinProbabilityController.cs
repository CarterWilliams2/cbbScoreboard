using cbbScoreboard.Models;
using cbbScoreboard.Services;
using Microsoft.AspNetCore.Mvc;

namespace cbbScoreboard.Controllers;

[ApiController]
[Route("api/win-probability")]
public class WinProbabilityController: ControllerBase
{
    private readonly WinProbabilityService _winProbabilityService;

    public WinProbabilityController(WinProbabilityService winProbabilityService)
    {
        _winProbabilityService = winProbabilityService;
    }

    [HttpPost]
    public async Task<ActionResult<WinProbabilityDto>> GetWinProbability(PlayByPlayDto play)
    {
        var plays = await _winProbabilityService.GetWinProbabilityAsync(play);
        return Ok(plays);
    }
    
}