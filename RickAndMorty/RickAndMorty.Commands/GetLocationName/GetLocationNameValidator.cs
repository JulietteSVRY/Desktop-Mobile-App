using FluentValidation;

namespace RickAndMorty.Commands.GetLocationName;

public class GetLocationNameValidator : AbstractValidator<GetLocationNameRequest>
{
    public GetLocationNameValidator()
    {
        RuleFor(x => x.Url)
            .Must(x => !string.IsNullOrWhiteSpace(x) && Uri.TryCreate(x, UriKind.Absolute, out _));
    }
}