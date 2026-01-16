namespace cbbScoreboard.Models;

public class GameStateDto
{
    public int AwayScore { get; set; } = 0;
    public int HomeScore { get; set; } = 0;

    public int Period { get; set; } = 0;

    public int TimeRemainingSeconds { get; set; } = 0;


};