using ScreenSound.Web.Requests;
using ScreenSound.Web.Response;
using System.Net.Http.Json;

namespace ScreenSound.Web.Services;

public class ArtistaAPI(IHttpClientFactory httpClient)
{
    public const string HttpClientName = "ArtistaAPI";
    private readonly HttpClient _httpClient = httpClient.CreateClient(HttpClientName);

    public async Task<ICollection<ArtistaResponse>?> GetArtistasAsync()
    {
        return await _httpClient.GetFromJsonAsync<ICollection<ArtistaResponse>>("artistas");
    }

    public async Task AddArtistaAsync(ArtistaRequest artist)
    {
        await _httpClient.PostAsJsonAsync("artistas", artist);
    }
}
