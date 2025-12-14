namespace cbbScoreboard.Models;

public class GameDto
{
    public string HomeTeam { get; set; } = string.Empty;
    public string AwayTeam { get; set; } = string.Empty;

    public int HomeScore { get; set; }
    public int AwayScore { get; set; }

    public string Status { get; set; } = string.Empty;
}