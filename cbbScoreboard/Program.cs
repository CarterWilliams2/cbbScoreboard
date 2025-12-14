using Microsoft.AspNetCore.Mvc.ApiExplorer;
using cbbScoreboard.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<GamesService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapGet("/api/games/today", async (GamesService gamesService) =>
{
    var games = await gamesService.GetTodayGamesAsync();
    return Results.Ok(games);
})
.WithName("GetTodayGames");

app.Run();