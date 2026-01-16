using System.Text.Json.Serialization;

namespace cbbScoreboard.Models;

public class WinProbabilityDto
{
    [JsonPropertyName("away_win_probability")]
    public double AwayWinProbability { get; set; }
    
    [JsonPropertyName("home_win_probability")]
    public double HomeWinProbability { get; set; }


};