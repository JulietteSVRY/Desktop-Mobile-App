using System.Text.Json.Serialization;

namespace RickAndMorty.Model.RickAndMortyApiJsonObjects;

public sealed record GetLocations
{
    [JsonPropertyName("info")]
    public Info Info { get; init; } = default!;

    [JsonPropertyName("results")]
    public ResultsLocation[] Results { get; init; } = Array.Empty<ResultsLocation>();
}

public class Info
{
    [JsonPropertyName("count")]
    public int Count { get; init; }
    [JsonPropertyName("pages")]
    public int Pages { get; init; }
    [JsonPropertyName("next")]
    public string Next { get; init; } = default!;
    [JsonPropertyName("prev")]
    public object Prev { get; init; } = default!;
}

public class ResultsLocation
{
    [JsonPropertyName("id")]
    public ulong Id { get; init; }
    [JsonPropertyName("name")]
    public string Name { get; init; } = default!;
    [JsonPropertyName("type")]
    public string Type { get; init; } = default!;
    [JsonPropertyName("dimension")]
    public string Dimension { get; init; } = default!;
    [JsonPropertyName("residents")]
    public string[] Residents { get; init; }  = Array.Empty<string>();
    [JsonPropertyName("url")]
    public string Url { get; init; } = default!;
    [JsonPropertyName("created")]
    public DateTimeOffset Created { get; init; }
}

