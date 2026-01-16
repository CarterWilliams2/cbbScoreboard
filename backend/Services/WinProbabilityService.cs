using cbbScoreboard.Models;
using System.Text;
using System.Text.Json;

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

        GameStateDto game_state = ConvertPlayToGameState(play);
        string game_state_string = JsonSerializer.Serialize(game_state);

        var url = "http://localhost:8000/predict";
        var content = new StringContent(
            game_state_string,
            Encoding.UTF8,
            "application/json"
        );

        var response = await _httpClient.PostAsync(url, content);

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
        var winProbability = JsonSerializer.Deserialize<WinProbabilityDto>(responseString);

        return winProbability;
    }

    private GameStateDto ConvertPlayToGameState(PlayByPlayDto play)
    {
        var away_score = int.Parse(play.VisitorScore);
        var home_score = int.Parse(play.HomeScore);
        var period = int.Parse(play.PeriodNumber);

        string[] clock_elements = play.Clock.Split(":");
        var minutes = int.Parse(clock_elements[0]);
        var seconds = int.Parse(clock_elements[1]);

        var time_remaining_seconds = seconds + minutes * 60;
        if (period == 1)
        {
            time_remaining_seconds += 20 * 60;
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