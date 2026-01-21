using Microsoft.AspNetCore.Mvc.ApiExplorer;
using cbbScoreboard.Services;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    var port = Environment.GetEnvironmentVariable("PORT");
    if (port != null)
    {
        options.ListenAnyIP(int.Parse(port));
    }
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("frontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .WithOrigins("http://cbb-scoreboard-frontend.s3-website.us-east-2.amazonaws.com/")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});



// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient<GamesService>();
builder.Services.AddHttpClient<StatsService>();
builder.Services.AddHttpClient<PlayByPlayService>();
builder.Services.AddHttpClient<WinProbabilityService>();
builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("frontend");
app.UseHttpsRedirection();

app.MapControllers();

var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Urls.Add($"http://*:{port}");

app.Run();