using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Application.Validation;

public sealed class ValidationBehavior<TRequest, TResult>(IValidator<TRequest> validator) 
    : IPipelineBehavior<TRequest, Result<TResult>> 
    where TRequest : notnull
{
    public async Task<Result<TResult>> Handle(TRequest request,
        RequestHandlerDelegate<Result<TResult>> next, 
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid)
            return await next();
        return CreateResultWithValidationFailures<TResult>(validationResult.Errors);
    }

    private static Result<T> CreateResultWithValidationFailures<T>(IEnumerable<ValidationFailure> validationFailures) => 
        Result.Fail<T>(validationFailures.Select(validationFailure => new Error(validationFailure.ErrorMessage)));
}