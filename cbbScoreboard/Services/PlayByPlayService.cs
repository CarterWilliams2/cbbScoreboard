using System.Dynamic;
using System.Net.Http;
using System.Text.Json;
using cbbScoreboard.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Globalization;
using System;
using System.Reflection.Metadata;


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
        



        return null;
    } 
}