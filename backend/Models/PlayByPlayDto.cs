namespace cbbScoreboard.Models;

public class PlayByPlayDto
{
    public string HomeScore { get; set; } = string.Empty;
    public string VisitorScore { get; set; } = string.Empty;

    public string PeriodNumber { get; set; } = string.Empty;
    public string Clock { get; set; } = string.Empty;

    public string EventDescription { get; set; } = string.Empty;

};