using System.ComponentModel.DataAnnotations;

namespace RickAndMorty.Model.Entity;

public abstract record BaseEntity
{
    [Key]
    public ulong Id { get; init; }
    public required DateTimeOffset CheatedAt { get; init; }
    public required DateTimeOffset ChangedAt { get; set; }
}