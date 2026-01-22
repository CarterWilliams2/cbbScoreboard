using cbbScoreboard.Models;
using System.Text;
using System.Text.Json;
using System.Net.Http.Json; 
using System.Text.Json;
using System.Text.Json.Serialization;

namespace cbbScoreboard.Services;

public class WinProbabilityService
{
    private readonly HttpClient _httpClient;
    private readonly string _predictUrl;
    
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    public WinProbabilityService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _predictUrl = configuration["MLService:PredictUrl"] 
                      ?? throw new ArgumentNullException("PredictUrl not found");
    }

    public async Task<WinProbabilityDto> GetWinProbabilityAsync(PlayByPlayDto play)
    {
        GameStateDto game_state = ConvertPlayToGameState(play);

        var response = await _httpClient.PostAsJsonAsync(_predictUrl, game_state, _jsonOptions);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"ML API Error: {response.StatusCode} - {error}");
        }

        // Deserialize using the same snake_case options
        return await response.Content.ReadFromJsonAsync<WinProbabilityDto>(_jsonOptions);
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