using System.Dynamic;
using System.Net.Http;
using System.Text.Json;
using cbbScoreboard.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

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

                Status = _game.GetProperty("gameState").GetString()?.ToLower() ?? "",
                StartTime = _game.GetProperty("startTime").GetString() ?? "",
                StartDate = _game.GetProperty("startDate").GetString() ?? "",

                HomeConference = home.GetProperty("conferences")[0].GetProperty("conferenceSeo").GetString() ?? "",
                AwayConference = away.GetProperty("conferences")[0].GetProperty("conferenceSeo").GetString() ?? ""
            });
        }
        return games;
    }

    public async Task<List<GameDto>> GetFilteredGamesAsync(
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
                "live" => games.Where(g => g.Status == "live").ToList(),
                "final" => games.Where(g => g.Status == "final").ToList(),
                "upcoming" => games
                    .Where(g => g.Status != "live" && g.Status != "final")
                    .ToList(),
                _ => games
            };
        }

        if (!string.IsNullOrWhiteSpace(conference))
        {
            conference = conference.ToLower();

            games = games
                .Where(g => g.AwayConference.ToLower() == conference)
                .ToList();
        }

        games = games
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return games;
    }

    public async Task<GameDto?> GetGameByIdAsync(string gameId)
    {
        var games = await GetTodayGamesAsync();

        return games.FirstOrDefault(g => g.GameId == gameId);

    }
}