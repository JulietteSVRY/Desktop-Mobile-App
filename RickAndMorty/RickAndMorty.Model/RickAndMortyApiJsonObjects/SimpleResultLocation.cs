using System.Text.Json.Serialization;

namespace RickAndMorty.Model.RickAndMortyApiJsonObjects;

public class SimpleResultLocation
{
    [JsonPropertyName("id")]
    public ulong Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("type")]
    public string Type { get; set; }
    [JsonPropertyName("dimension")]
    public string Dimension { get; set; }
    [JsonPropertyName("url")]
    public string Url { get; set; }
    [JsonPropertyName("created")]
    public DateTimeOffset Created { get; set; }
}