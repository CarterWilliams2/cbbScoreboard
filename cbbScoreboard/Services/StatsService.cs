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

public class StatsService
{
    private readonly HttpClient _httpClient;

    public StatsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<StatDto>> GetStatsAsync(string path, string statName) {

        //for now
        var temp_path = "individual/605";
        var temp_statName = "AST";

        var url = "https://ncaa-api.henrygd.me/stats/basketball-men/d1/current/individual/605";
        var response  = await _httpClient.GetStringAsync(url);
        using var doc = JsonDocument.Parse(response);

        var stats = ParseStats(doc, temp_statName);
        return stats;
    }

    private List<StatDto> ParseStats(JsonDocument doc, string statName)
    {
        var stats = new List<StatDto>();

        var statsArray = doc.RootElement
            .GetProperty("data")
            .EnumerateArray();

        foreach (var stat in statsArray)
        {
            stats.Add(new StatDto
            {
                rank = stat.GetProperty("Rank").GetString() ?? "",
                player_name = stat.GetProperty("Name").GetString() ?? "",
                player_team = stat.GetProperty("Team").GetString() ?? "",
                player_class = stat.GetProperty("Cl").GetString() ?? "",
                player_height = stat.GetProperty("Height").GetString() ?? "",
                player_position = stat.GetProperty("Position").GetString() ?? "",
                games_played = stat.GetProperty("G").GetString() ?? "",
                stat_total = stat.GetProperty(statName).GetString() ?? ""

            });
        }
        return stats;
    }
}