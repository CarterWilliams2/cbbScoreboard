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

    
    
}