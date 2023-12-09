using FluentValidation;
using MediatR;

namespace RickAndMorty.Commands.Pipelines;

public class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest> validator) : IPipelineBehavior<TRequest, TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if(validator is null)
            return await next();
        
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
            throw new ValidationException(validationResult.Errors);

        return await next();
    }
}