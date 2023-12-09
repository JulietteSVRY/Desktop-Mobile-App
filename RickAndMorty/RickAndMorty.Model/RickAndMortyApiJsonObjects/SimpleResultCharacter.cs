using System.Text.Json.Serialization;

namespace RickAndMorty.Model.RickAndMortyApiJsonObjects;

public class SimpleResultCharacter
{
    [JsonPropertyName("id")]
    public ulong Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("status")]
    public string Status { get; set; }
    [JsonPropertyName("species")]
    public string Species { get; set; }
    [JsonPropertyName("type")]
    public string Type { get; set; }
    [JsonPropertyName("gender")]
    public string Gender { get; set; }
    [JsonPropertyName("origin")]
    public Origin Origin { get; set; }
    [JsonPropertyName("location")]
    public Location Location { get; set; }
    [JsonPropertyName("image")]
    public string Image { get; set; }
    [JsonPropertyName("episode")]
    public string[] Episode { get; set; }
    [JsonPropertyName("url")]
    public string Url { get; set; }
    [JsonPropertyName("created")]
    public DateTimeOffset Created { get; set; }
}