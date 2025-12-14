using System.Net.Http;
using System.Text.Json;

namespace cbbScoreboard.Services;

public class GamesService
{
    private readonly HttpClient _httpClient;

    public GamesService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<JsonElement> GetTodayGamesAsync()
    {
        var url = "https://ncaa-api.henrygd.me/scoreboard/basketball-men/d1";

        var response  = await _httpClient.GetStringAsync(url);

        using var doc = JsonDocument.Parse(response);
        return doc.RootElement.Clone();
    }
}