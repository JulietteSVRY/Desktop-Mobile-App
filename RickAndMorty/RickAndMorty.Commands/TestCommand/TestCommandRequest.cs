using MediatR;

namespace RickAndMorty.Commands.TestCommand;

public record TestCommandRequest : IRequest<TestCommandResponse>
{
    public required string Message { get; init; }
}