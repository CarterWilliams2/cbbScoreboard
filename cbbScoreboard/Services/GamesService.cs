using System.Dynamic;
using System.Net.Http;
using System.Text.Json;
using cbbScoreboard.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Globalization;
using System;


namespace cbbScoreboard.Services;

public class GamesService
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _cache;

    private const string TodayGamesCacheKey = "today_games";

    public GamesService(HttpClient httpClient, IMemoryCache cache)
    {
        _httpClient = httpClient;
        _cache = cache;
    }

    public async Task<List<GameDto>> GetTodayGamesAsync()
    {

        if (_cache.TryGetValue(TodayGamesCacheKey, out List<GameDto>? cachedGames))
    {
        return cachedGames!;
    }

        var url = "https://ncaa-api.henrygd.me/scoreboard/basketball-men/d1";
        var response  = await _httpClient.GetStringAsync(url);
        using var doc = JsonDocument.Parse(response);

        var games = ParseGames(doc);

        _cache.Set(
            TodayGamesCacheKey,
            games,
            TimeSpan.FromSeconds(30)
        );

        return games;

        
    }

    private List<GameDto> ParseGames(JsonDocument doc)
    {
        var games = new List<GameDto>();

        var gamesArray = doc.RootElement
            .GetProperty("games")
            .EnumerateArray();

        foreach (var game in gamesArray)
        {
            var _game = game.GetProperty("game");
            var home = _game.GetProperty("home");
            var away = _game.GetProperty("away");

            games.Add(new GameDto
            {
                GameId = _game.GetProperty("gameID").GetString() ?? "",
                HomeTeam = home.GetProperty("names").GetProperty("short").GetString() ?? "",
                AwayTeam = away.GetProperty("names").GetProperty("short").GetString() ?? "",

                HomeScore = home.GetProperty("score").GetString() ?? "",
                AwayScore = away.GetProperty("score").GetString() ?? "",

                Status = NormalizeStatus(_game.GetProperty("gameState").GetString()),

                StartTimeUtc = ParseStartTime(
                    _game.GetProperty("startDate").GetString(),
                    _game.GetProperty("startTime").GetString()
                ),

                HomeConference = home.GetProperty("conferences")[0].GetProperty("conferenceSeo").GetString() ?? "",
                AwayConference = away.GetProperty("conferences")[0].GetProperty("conferenceSeo").GetString() ?? "",

                Clock = _game.GetProperty("contestClock").GetString() ?? ""
            });
        }
        return games;
    }

    public async Task<PagedResult<GameDto>> GetFilteredGamesAsync(
        string? status,
        string? conference,
        int page,
        int pageSize
    )
    {
        var games = await GetTodayGamesAsync();

        if (!string.IsNullOrWhiteSpace(status))
        {
            status = status.ToLower();

            games = status switch
            {
                "live" => games.Where(g => g.Status == GameStatus.Live).ToList(),
                "final" => games.Where(g => g.Status == GameStatus.Final).ToList(),
                "upcoming" => games
                    .Where(g => g.Status != GameStatus.Live && g.Status != GameStatus.Final)
                    .ToList(),
                _ => games
            };
        }

        if (!string.IsNullOrWhiteSpace(conference))
        {
            conference = conference.ToLower();

            games = games
                .Where(g => (g.AwayConference.ToLower() == conference || g.HomeConference.ToLower() == conference))
                .ToList();
        }

        games = games
            .OrderBy(g => g.StartTimeUtc)
            .ToList();

        var totalCount = games.Count;

        var items = games
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PagedResult<GameDto> {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

    }

    public async Task<GameDto?> GetGameByIdAsync(string gameId)
    {
        var games = await GetTodayGamesAsync();

        return games.FirstOrDefault(g => g.GameId == gameId);

    }

    private GameStatus NormalizeStatus(string? raw)
    {
        return raw?.ToLower() switch
        {
            "live" => GameStatus.Live,
            "final" => GameStatus.Final,
            _ => GameStatus.Upcoming
        };
    }

    private static DateTime ParseStartTime(string? date, string? time)
{
    if (string.IsNullOrWhiteSpace(date) || string.IsNullOrWhiteSpace(time))
        return DateTime.MinValue;

    time = time.Replace(" ET", "").Trim();

    var combined = $"{date} {time}";

    if (DateTime.TryParseExact(
        combined,
        "MM/dd/yyyy h:mm tt",
        CultureInfo.InvariantCulture,
        DateTimeStyles.None,
        out var parsed))
    {
        var eastern = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
        return TimeZoneInfo.ConvertTimeToUtc(parsed, eastern);
    }

    return DateTime.MinValue;
}

}