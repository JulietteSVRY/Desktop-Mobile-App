using System.Text.Json;
using RickAndMorty.Abstractions.HttpClients;
using RickAndMorty.Model.RickAndMortyApiJsonObjects;

namespace RickAndMorty.Infrastructure.HttpClients;

public sealed class RickAndMortyHttpClient : IRickAndMortyHttpClient
{
    private const string BaseUrl = "https://rickandmortyapi.com/api/";
    private readonly HttpClient _httpClient;

    public RickAndMortyHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(BaseUrl);
    }


    public async Task<GetLocations[]> GetLocations(CancellationToken cancellationToken = default)
    {
        const string url = "location";
        var locationResponses = new List<GetLocations>();
        string nextUrl;
        using (var httpResponseMessage = await _httpClient.GetAsync(url, cancellationToken))
        {
            httpResponseMessage.EnsureSuccessStatusCode();
            var getLocationResponse = await JsonSerializer.DeserializeAsync<GetLocations>(await httpResponseMessage.Content.ReadAsStreamAsync(cancellationToken), cancellationToken: cancellationToken)
                                      ?? throw new Exception("Не удалось серилизовать локации из апи");
            locationResponses.Add(getLocationResponse);
            nextUrl = getLocationResponse.Info.Next;
        }
        
        //load next location
        if (string.IsNullOrWhiteSpace(nextUrl))
            return locationResponses.ToArray();
        
        while (!string.IsNullOrWhiteSpace(nextUrl))
        {
            using var httpResponseMessageNext = await _httpClient.GetAsync(nextUrl, cancellationToken);
            httpResponseMessageNext.EnsureSuccessStatusCode();
            var getLocationResponseNext = await JsonSerializer.DeserializeAsync<GetLocations>(await httpResponseMessageNext.Content.ReadAsStreamAsync(cancellationToken), cancellationToken: cancellationToken)
                                          ?? throw new Exception("Не удалось серилизовать локации из апи");
            locationResponses.Add(getLocationResponseNext);
            nextUrl = getLocationResponseNext.Info.Next;
        }
        return locationResponses.ToArray();
    }

    public async Task<GetCharacter[]> GetCharacters(CancellationToken cancellationToken = default)
    {
        const string url = "character";
        var locationResponses = new List<GetCharacter>();
        string nextUrl;
        using (var httpResponseMessage = await _httpClient.GetAsync(url, cancellationToken))
        {
            httpResponseMessage.EnsureSuccessStatusCode();
            var getLocationResponse = await JsonSerializer.DeserializeAsync<GetCharacter>(await httpResponseMessage.Content.ReadAsStreamAsync(cancellationToken), cancellationToken: cancellationToken)
                                      ?? throw new Exception("Не удалось серилизовать локации из апи");
            locationResponses.Add(getLocationResponse);
            nextUrl = getLocationResponse.Info.Next;
        }
        
        //load next location
        if (string.IsNullOrWhiteSpace(nextUrl))
            return locationResponses.ToArray();
        
        while (!string.IsNullOrWhiteSpace(nextUrl))
        {
            using var httpResponseMessageNext = await _httpClient.GetAsync(nextUrl, cancellationToken);
            httpResponseMessageNext.EnsureSuccessStatusCode();
            var getLocationResponseNext = await JsonSerializer.DeserializeAsync<GetCharacter>(await httpResponseMessageNext.Content.ReadAsStreamAsync(cancellationToken), cancellationToken: cancellationToken)
                                          ?? throw new Exception("Не удалось серилизовать локации из апи");
            locationResponses.Add(getLocationResponseNext);
            nextUrl = getLocationResponseNext.Info.Next;
        }
        return locationResponses.ToArray();
    }

    public async Task<string?> GetLocationName(string url, CancellationToken cancellationToken = default)
    {
        using var httpResponseMessage = await _httpClient.GetAsync(url, cancellationToken);
        var jsonDocument = await JsonDocument.ParseAsync(await httpResponseMessage.Content.ReadAsStreamAsync(cancellationToken), cancellationToken: cancellationToken);
        var locationName = jsonDocument.RootElement.GetProperty("name").GetString();
        return locationName;
    }

    public async Task<SimpleResultCharacter[]> GetCharacterByIds(ulong[] ids, CancellationToken cancellationToken = default)
    {
        var url = $"character/{string.Join(',', ids)}";
        using var httpResponseMessage = await _httpClient.GetAsync(url, cancellationToken);
        SimpleResultCharacter[] resultsCharacter;
        if (ids.Length == 1)
        {
            var location = await JsonSerializer.DeserializeAsync<SimpleResultCharacter>(await httpResponseMessage.Content.ReadAsStreamAsync(cancellationToken), cancellationToken: cancellationToken)
                           ?? throw new Exception("Не удалось серилизовать локации из апи");
            resultsCharacter = new[]
            {
                location
            };
        }
        else
            resultsCharacter = await JsonSerializer.DeserializeAsync<SimpleResultCharacter[]>(await httpResponseMessage.Content.ReadAsStreamAsync(cancellationToken), cancellationToken: cancellationToken)
                               ?? throw new Exception("Не удалось серилизовать локации из апи");
        return resultsCharacter;
    }

    public async Task<SimpleResultLocation[]> GetLocationByIds(ulong[] ids, CancellationToken cancellationToken = default)
    {
        var url = $"location/{string.Join(',', ids)}";
        using var httpResponseMessage = await _httpClient.GetAsync(url, cancellationToken);
        SimpleResultLocation[] resultLocation;
        if (ids.Length == 1)
        {
            var location = await JsonSerializer.DeserializeAsync<SimpleResultLocation>(await httpResponseMessage.Content.ReadAsStreamAsync(cancellationToken), cancellationToken: cancellationToken)
                         ?? throw new Exception("Не удалось серилизовать локации из апи");
            resultLocation = new[]
            {
                location
            };
        }
        else
         resultLocation = await JsonSerializer.DeserializeAsync<SimpleResultLocation[]>(await httpResponseMessage.Content.ReadAsStreamAsync(cancellationToken), cancellationToken: cancellationToken)
                         ?? throw new Exception("Не удалось серилизовать локации из апи");
        return resultLocation;
    }
}   