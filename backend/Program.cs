
using cbbScoreboard.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("frontend", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5173",
                "http://cbb-scoreboard-frontend.s3-website.us-east-2.amazonaws.com"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

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

// app.UseHttpsRedirection();

app.MapControllers();

app.Run();
