using FluentValidation.Results;

namespace Domain.Common.Extensions;

public static class ValidationResultExtensions
{
    public static bool IsNotValid(this ValidationResult validationResult) => !validationResult.IsValid;

    public static IEnumerable<object> ToReadableFormat(
        this ValidationResult validationResult) =>
        validationResult.Errors.Select(e =>
            new
            {
                errorCode = e.ErrorCode,
                propertyName = e.PropertyName,
                errorMessage = e.ErrorMessage
            });
}