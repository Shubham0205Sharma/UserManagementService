using FluentValidation;
using FluentValidation.Results;
using UserManagement.Application.Responses;

namespace UserManagement.Application.Extensions
{
    public static class ValidationExtensions
    {
        public static ValidationResult Validate<T>(this T model, IValidator<T> validator)
        {
            return validator.Validate(model);
        }

        public static ApiResponse<T> ToApiResponse<T>(this ValidationResult validationResult, string message)
        {
            return new ApiResponse<T>(message, validationResult.Errors.Select(e => new ErrorDetails
            {
                Field = e.PropertyName,
                Error = e.ErrorMessage
            }).ToList());
        }
    }

}
