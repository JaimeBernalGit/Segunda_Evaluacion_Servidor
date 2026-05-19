using Models;
namespace CursosAPI.Services;
public class FactService : IFactService
{
    private readonly HttpClient _httpClient;

    public FactService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetRandomFactAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<FactResponse>(
            "https://uselessfacts.jsph.pl/api/v2/facts/random?language=en"
        );
        return response?.Text ?? "No fact available";
    }
}