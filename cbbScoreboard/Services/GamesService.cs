using System.Dynamic;
using System.Net.Http;
using System.Text.Json;
using cbbScoreboard.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace cbbScoreboard.Services;

public class GamesService
{
    private readonly HttpClient _httpClient;

    public GamesService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<GameDto>> GetTodayGamesAsync()
    {
        var url = "https://ncaa-api.henrygd.me/scoreboard/basketball-men/d1";

        var response  = await _httpClient.GetStringAsync(url);
        using var doc = JsonDocument.Parse(response);

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
        string? conference
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

        return games;
    }

    internal async Task<ActionResult<GameDto?>> GetGameByIdAsync(string gameId)
    {
        var games = await GetTodayGamesAsync();

        return games.FirstOrDefault(g => g.GameId == gameId);

    }
}