namespace cbbScoreboard.Models;

public class StatDto 
{
    public int rank { get; set; } = 0;
    public string player_name { get; set; } = string.Empty;
    public string player_team { get; set; } = string.Empty;
    public string player_class { get; set; } = string.Empty;
    public string player_height { get; set; } = string.Empty;
    public string player_position { get; set; } = string.Empty;
    public string games_played { get; set; } = string.Empty;
    public string stat_total { get; set; } = string.Empty;
}