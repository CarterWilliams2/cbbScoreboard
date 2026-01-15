using System.Text.Json;
using cbbScoreboard.Models;


namespace cbbScoreboard.Services;

public class PlayByPlayService
{
    private readonly HttpClient _httpClient;

    public PlayByPlayService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<PlayByPlayDto>> GetPlayByPlayAsync(string gameId)
    {

        var url = $"https://ncaa-api.henrygd.me/game/{gameId}/play-by-play";
        var response  = await _httpClient.GetStringAsync(url);
        using var doc = JsonDocument.Parse(response);

        var plays = ParsePlayByPlay(doc);

        return plays;

    }

    private List<PlayByPlayDto> ParsePlayByPlay(JsonDocument doc)
    {
        var plays = new List<PlayByPlayDto>();

        var periodsArray = doc.RootElement
            .GetProperty("periods")
            .EnumerateArray();
        
        foreach (var period in periodsArray)
        {
            var playByPlayStatsArray = period.GetProperty("playbyplayStats").EnumerateArray();
            var periodNumber = period.GetProperty("periodNumber").ToString();

            foreach (var play in playByPlayStatsArray)
            {
                
                plays.Add(new PlayByPlayDto
                {
                    HomeScore = play.GetProperty("homeScore").ToString(),
                    VisitorScore = play.GetProperty("visitorScore").ToString(),
                    PeriodNumber = periodNumber,
                    Clock = play.GetProperty("clock").ToString(),
                    EventDescription = play.GetProperty("eventDescription").ToString(),

                });
            }
        }
        
        return plays;
    } 
}