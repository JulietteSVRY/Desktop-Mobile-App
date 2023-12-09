using System.ComponentModel.DataAnnotations.Schema;

namespace RickAndMorty.Model.Entity;

[Table("Users")]
public sealed record User : BaseEntity
{
    public required string Login { get; init; }
    public required string Password { get; init; }
    public required bool IsLogin { get; set; }
}