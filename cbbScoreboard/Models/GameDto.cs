namespace cbbScoreboard.Models;

public class GameDto
{
    public string GameId { get; set; } = string.Empty;
    public string HomeTeam { get; set; } = string.Empty;
    public string AwayTeam { get; set; } = string.Empty;

    public string HomeScore { get; set; } = string.Empty;
    public string AwayScore { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;
    public string StartTime { get; set; } = string.Empty;
    public string StartDate { get; set; } = string.Empty;

    public string HomeConference { get; set; } = string.Empty;
    public string AwayConference { get; set; } = string.Empty;
}