using FluentValidation;

namespace RickAndMorty.Commands.GetLikeds;

public class GetLikedsValidator : AbstractValidator<GetLikedsRequest>
{
    public GetLikedsValidator()
    {
        RuleFor(x => x.Limit).GreaterThan(0).When(x => x.Limit.HasValue);
        RuleFor(x => x.Skip).GreaterThanOrEqualTo(0).When(x => x.Skip.HasValue);
    }
}