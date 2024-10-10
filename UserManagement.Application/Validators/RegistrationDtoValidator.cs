using FluentValidation;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Validators
{
    public class RegistrationDtoValidator : AbstractValidator<RegistrationDto>
    {
        public RegistrationDtoValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Valid email is required");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"\d").WithMessage("Password must contain at least one number.")
                .Matches(@"[^\da-zA-Z]").WithMessage("Password must contain at least one special character.");
           RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Confirm Password is required.");
           RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage("Password and Confirm Password do not match.");
        }
    }
}
