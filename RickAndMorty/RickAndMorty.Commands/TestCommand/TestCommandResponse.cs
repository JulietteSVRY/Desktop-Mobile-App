namespace RickAndMorty.Commands.TestCommand;

public record TestCommandResponse
{
    public required string Message { get; init; }
}