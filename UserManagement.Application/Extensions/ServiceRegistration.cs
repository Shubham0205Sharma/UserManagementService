using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Application.DTOs;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Mapper;
using UserManagement.Application.Services;
using UserManagement.Application.Validators;

namespace UserManagement.Application.Extensions
{
    public static class ServiceRegistration
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddTransient<IValidator<UserDto>, UserDtoValidator>();
            services.AddTransient<IValidator<RegistrationDto>, RegistrationDtoValidator>();
            services.AddTransient<IValidator<LoginDto>, LoginDtoValidator>();
            services.AddTransient<IValidator<RoleDto>, RoleDtoValidator>();
            services.AddTransient<IValidator<UpdateRoleDto>, UpdateRoleDtoValidator>();
            services.AddTransient<IValidator<PasswordUpdateDto>, PasswordUpdateDtoValidator>();





        }
    }
}
