using System.Text.Json.Serialization;

namespace RickAndMorty.Model.RickAndMortyApiJsonObjects;

public sealed record GetCharacter
{
    [JsonPropertyName("info")]
    public required InfoCharacter Info { get; set; }
    [JsonPropertyName("results")]
    public required ResultsCharacter[] Results { get; set; }
}

public record InfoCharacter
{
    [JsonPropertyName("count")]
    public int Count { get; set; }
    [JsonPropertyName("pages")]
    public int Pages { get; set; }
    [JsonPropertyName("next")]
    public required string Next { get; set; }
    [JsonPropertyName("prev")]
    public required string Prev { get; set; }
}

public record ResultsCharacter
{
    [JsonPropertyName("id")]
    public ulong Id { get; set; }
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("status")]
    public required string Status { get; set; }
    [JsonPropertyName("species")]
    public required string Species { get; set; }
    [JsonPropertyName("type")]
    public required string Type { get; set; }
    [JsonPropertyName("gender")]
    public required string Gender { get; set; }
    [JsonPropertyName("origin")]
    public required Origin Origin { get; set; }
    [JsonPropertyName("location")]
    public required Location Location { get; set; }
    [JsonPropertyName("image")]
    public required string Image { get; set; }
    [JsonPropertyName("episode")]
    public required string[] Episode { get; set; }
    [JsonPropertyName("url")]
    public required string Url { get; set; }
    [JsonPropertyName("created")]
    public required string Created { get; set; }
}

public record Origin
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("url")]
    public required string Url { get; set; }
}

public record Location
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("url")]
    public required string Url { get; set; }
}

