using MediatR;

namespace RickAndMorty.Commands.TestCommand;

public class TestCommandHandler : IRequestHandler<TestCommandRequest,TestCommandResponse>
{
    public Task<TestCommandResponse> Handle(TestCommandRequest request, CancellationToken cancellationToken) =>
        Task.FromResult(new TestCommandResponse
        {
            Message = $"RESPONSE: {request.Message}"
        });
}