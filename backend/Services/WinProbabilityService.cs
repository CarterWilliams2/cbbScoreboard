using cbbScoreboard.Models;

namespace cbbScoreboard.Services;

public class WinProbabilityService
{
    private readonly HttpClient _httpClient;

    public WinProbabilityService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WinProbabilityDto> GetWinProbabilityAsync(PlayByPlayDto play)
    {
        // convert play into a gamestate

        var url = "http://localhost:8000/predict";
        var response  = await _httpClient.GetStringAsync(url);
        // parse/shape response
        return null;
    }

    private GameStateDto ConvertPlayToGameState(PlayByPlayDto play)
    {
        var away_score = int.Parse(play.VisitorScore);
        var home_score = int.Parse(play.HomeScore);
        var period = int.Parse(play.PeriodNumber);

        string[] clock_elements = play.Clock.Split(":");
        var minutes = int.Parse(clock_elements[0]);
        var seconds = int.Parse(clock_elements[1]);

        var time_remaining_seconds = seconds + minutes*60;
        if (period == 1)
        {
            time_remaining_seconds += 20*60;
        }

        return new GameStateDto
        {
          AwayScore = away_score,
          HomeScore = home_score,
          Period = period,
          TimeRemainingSeconds = time_remaining_seconds,  
        };

    }

   
}