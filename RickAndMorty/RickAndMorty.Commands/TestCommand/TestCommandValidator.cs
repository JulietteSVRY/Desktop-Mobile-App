using FluentValidation;

namespace RickAndMorty.Commands.TestCommand;

public class TestCommandValidator : AbstractValidator<TestCommandRequest>
{
    public TestCommandValidator()
    {
        RuleFor(x => x.Message).NotEmpty();
    }
}