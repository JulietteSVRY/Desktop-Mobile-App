using FluentValidation;

namespace RickAndMorty.Commands.DeleteLiked;

public class DeleteLikedValidator : AbstractValidator<DeleteLikedRequest>
{
    public DeleteLikedValidator()
    {
        RuleFor(x => x.LikedType).IsInEnum();
    }
}