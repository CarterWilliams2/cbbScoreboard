namespace cbbScoreboard.Models;

public class PlayByPlayDto
{
    public string GameId { get; set; } = string.Empty;
    public string Score { get; set; } = string.Empty;

    public int PeriodNumber { get; set; } = 0;
    public string Clock { get; set; } = string.Empty;

    public string EventDescription { get; set; } = string.Empty;

};