namespace cbbScoreboard.Models;

public class GameDto
{
    public string GameId { get; set; } = string.Empty;
    public string HomeTeam { get; set; } = string.Empty;
    public string AwayTeam { get; set; } = string.Empty;

    public string HomeScore { get; set; } = string.Empty;
    public string AwayScore { get; set; } = string.Empty;

    public GameStatus Status { get; set; }

    public DateTime StartTimeUtc { get; set; }

    public string HomeConference { get; set; } = string.Empty;
    public string AwayConference { get; set; } = string.Empty;

    public string Clock { get; set; } = string.Empty;
    public string Period { get; set; } = string.Empty;
}