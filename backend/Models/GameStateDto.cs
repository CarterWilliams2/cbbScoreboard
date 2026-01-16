using System.Text.Json.Serialization;

namespace cbbScoreboard.Models;

public class GameStateDto
{
    [JsonPropertyName("away_score")]
    public int AwayScore { get; set; } = 0;

    [JsonPropertyName("home_score")]
    public int HomeScore { get; set; } = 0;

    [JsonPropertyName("period")]
    public int Period { get; set; } = 0;

    [JsonPropertyName("time_remaining_seconds")]
    public int TimeRemainingSeconds { get; set; } = 0;


};