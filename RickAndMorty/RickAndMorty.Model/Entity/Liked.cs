using System.ComponentModel.DataAnnotations.Schema;

namespace RickAndMorty.Model.Entity;

[Table("Likeds")]
public sealed record Liked : BaseEntity
{
    public required ulong ReferenceId { get; init; }
    public required LikedType Type { get; init; }
    public required bool IsLiked { get; set; }
}