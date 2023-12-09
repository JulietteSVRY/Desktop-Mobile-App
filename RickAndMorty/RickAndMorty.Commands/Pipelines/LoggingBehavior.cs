﻿using MediatR;
using Microsoft.Extensions.Logging;

namespace RickAndMorty.Commands.Pipelines;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger = logger ?? throw new NullReferenceException("Не найден логгер!");

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling {Name}", typeof(TRequest).Name);
        var response = await next();
        _logger.LogInformation("Handled {Name}", typeof(TResponse).Name);

        return response;
    }
}