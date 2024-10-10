using FluentValidation;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Validators
{
    public class UpdateRoleDtoValidator : AbstractValidator<UpdateRoleDto>
    {
        public UpdateRoleDtoValidator()
        {
            RuleFor(x => x.OldRoleName).NotEmpty().WithMessage("OldRoleName is required");
            RuleFor(x => x.NewRoleName).NotEmpty().WithMessage("NewRoleName is required");


        }
    }
}
