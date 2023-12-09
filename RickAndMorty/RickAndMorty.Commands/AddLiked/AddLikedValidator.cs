using FluentValidation;

namespace RickAndMorty.Commands.AddLiked;

public class AddLikedValidator : AbstractValidator<AddLikedRequest>
{
    public AddLikedValidator()
    {
        RuleFor(x => x.LikedType).IsInEnum();
    }
}