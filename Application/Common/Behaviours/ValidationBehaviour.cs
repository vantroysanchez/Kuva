using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog;
using ValidationException = Application.Common.Exceptions.ValidationException;

namespace Application.Common.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : MediatR.IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly ILogger<IValidator<TRequest>> _logger;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators, ILogger<IValidator<TRequest>> logger)
    {
        _validators = validators;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                _validators.Select(v =>
                    v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .Where(r => r.Errors.Any())
                .SelectMany(r => r.Errors)
                .ToList();

            if (failures.Any())
            {
                _logger.LogError("Han ocurrido errores de validaciones");
                throw new ValidationException(failures);                
            }            
        }
        return await next();
    }
}

