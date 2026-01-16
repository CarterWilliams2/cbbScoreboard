namespace cbbScoreboard.Services;

public class WinProbabilityService
{
    private readonly HttpClient _httpClient;

    public WinProbabilityService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WinProbabilityDto> GetWinProbabilityAsync()
    {
        var url = "http://localhost:8000/predict";
        var response  = await _httpClient.GetStringAsync(url);
        // parse/shape response
        return null;
    }

   
}