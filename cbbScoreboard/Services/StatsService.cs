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


}