using FluentValidation;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Validators
{
    public class PasswordUpdateDtoValidator : AbstractValidator<PasswordUpdateDto>
    {
        public PasswordUpdateDtoValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
            RuleFor(x => x.NewPassword).NotEmpty().WithMessage("New password is required.");
            RuleFor(x => x.CurrentPassword).NotEmpty().WithMessage("Current password is required.");
        }
    }
}
