using ScreenSound.Web.Requests;
using ScreenSound.Web.Response;
using System.Net.Http.Json;
using System.Text.Json;

namespace ScreenSound.Web.Services;

public class ArtistaAPI(IHttpClientFactory httpClient)
{
    public const string HttpClientName = "API";
    private readonly HttpClient _httpClient = httpClient.CreateClient(HttpClientName);

    public async Task<ICollection<ArtistaResponse>?> GetArtistasAsync()
    {
        return await _httpClient.GetFromJsonAsync<ICollection<ArtistaResponse>>("artistas");
    }

    public async Task AddArtistaAsync(ArtistaRequest artist)
    {
        await _httpClient.PostAsJsonAsync("artistas", artist);
    }

    public async Task DeleteArtistaAsync(int id)
    {
        await _httpClient.DeleteAsync($"artistas/{id}");
    }

    public async Task<ArtistaResponse?> GetArtistaByNameAsync(string name)
    {
        var response = await _httpClient.GetAsync($"artistas/{name}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var artist = JsonSerializer.Deserialize<ArtistaResponse>(content, options);
            return artist;
        }
        else
            return null;
    }
}
