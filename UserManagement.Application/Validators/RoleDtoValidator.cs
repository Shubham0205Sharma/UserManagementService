using FluentValidation;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Validators
{
    public class RoleDtoValidator : AbstractValidator<RoleDto>
    {
        public RoleDtoValidator()
        {
            RuleFor(x => x.RoleName).NotEmpty().WithMessage("Role is required");

        }
    }
}
