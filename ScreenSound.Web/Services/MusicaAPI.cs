using ScreenSound.Web.Requests;
using ScreenSound.Web.Response;
using System.Formats.Asn1;
using System.Net.Http.Json;
using System.Text.Json;

namespace ScreenSound.Web.Services;

public class MusicaAPI
{
    private readonly HttpClient _httpClient;

    public MusicaAPI(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("API");
    }

    public async Task<ICollection<MusicaResponse>?> GetMusicasAsync()
    {
        return await _httpClient.GetFromJsonAsync<ICollection<MusicaResponse>>("musicas");
    }

    public async Task AddMusicaAsync(MusicaRequest musica)
    {
        await _httpClient.PostAsJsonAsync("musicas", musica);
    }

    public async Task DeleteMusicaAsync(int id)
    {
        await _httpClient.DeleteAsync($"musicas/{id}");
    }

    public async Task UpdateMusicaAsync(MusicaRequestEdit musica)
    {
        await _httpClient.PutAsJsonAsync($"musicas", musica);
    }

    public async Task<MusicaResponse?> GetMusicaByNameAsync(string name)
    {
        var response = await _httpClient.GetAsync($"musicas/{name}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var music = JsonSerializer.Deserialize<MusicaResponse>(content, options);
            return music;
        }
        else
            return null;
    }
}
