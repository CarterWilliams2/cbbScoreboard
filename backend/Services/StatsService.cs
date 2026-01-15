using System.Dynamic;
using System.Net.Http;
using System.Text.Json;
using cbbScoreboard.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Globalization;
using System;
using cbbScoreboard.Constants;


namespace cbbScoreboard.Services;

public class StatsService
{
    private readonly HttpClient _httpClient;

    public StatsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<StatDto>> GetStatsAsync(string statKey) {

    if (!StatCategories.Map.TryGetValue(statKey.ToLower(), out var statInfo))
        throw new ArgumentException("Invalid stat category");

    string categoryId = statInfo.categoryId;
    string stat = statInfo.stat;

    Console.WriteLine($"StatKey: {statKey}");
    Console.WriteLine($"CategoryId: {categoryId}");
    Console.WriteLine($"Stat: {stat}");

    var url = $"https://ncaa-api.henrygd.me/stats/basketball-men/d1/current/individual/{categoryId}";
    
    Console.WriteLine(url);
    var response  = await _httpClient.GetStringAsync(url);
    using var doc = JsonDocument.Parse(response);

    var stats = ParseStats(doc, stat);
    return stats;
}

    private List<StatDto> ParseStats(JsonDocument doc, string statName)
    {
        var stats = new List<StatDto>();

        var statsArray = doc.RootElement
            .GetProperty("data")
            .EnumerateArray();

        var current_rank = 0;

        foreach (var stat in statsArray)
        {
            
            stats.Add(new StatDto
            {
                rank = current_rank,
                player_name = stat.GetProperty("Name").GetString() ?? "",
                player_team = stat.GetProperty("Team").GetString() ?? "",
                player_class = stat.GetProperty("Cl").GetString() ?? "",
                player_height = stat.GetProperty("Height").GetString() ?? "",
                player_position = stat.GetProperty("Position").GetString() ?? "",
                games_played = stat.GetProperty("G").GetString() ?? "",
                stat_total = stat.GetProperty(statName).GetString() ?? ""

            });

            current_rank++;
        }
        return stats;
    }
}