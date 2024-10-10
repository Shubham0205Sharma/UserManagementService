using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManagement.Application.DTOs;
using UserManagement.Common.Exceptions;
using UserManagement.Application.Extensions;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Responses;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Interfaces;

namespace UserManagement.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IValidator<LoginDto> _loginDtoValidator;


        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration, IValidator<LoginDto> loginDtoValidator)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _loginDtoValidator = loginDtoValidator;
        }

        public async Task<ApiResponse<string>> LoginAsync(LoginDto loginDto)
        {
            try
            {
                var validationResult = loginDto.Validate(_loginDtoValidator);
                if (!validationResult.IsValid)
                {
                    return validationResult.ToApiResponse<string>("Validation failed");
                }

                var result = await _unitOfWork.Users.SignInWithPwdAsync(loginDto.Username, loginDto.Password);
                if (!result.Succeeded)
                    throw new CustomAppException("Login denied. Either username or password is incorrect.");

                var user = await _unitOfWork.Users.GetUserByNameAsync(loginDto.Username);
                if (user == null)
                    throw new CustomAppException($"No user found with for {loginDto.Username}.");
                var roles = await _unitOfWork.Users.GetUserRoleAsync(user);
                return new ApiResponse<string>(true, "Login Successful.", GenerateJwtToken(user, roles));
            }
            catch (CustomAppException cex)
            {
                return cex.ToApiResponse<string>();
            }
            catch (Exception ex)
            {
                return ex.ToApiResponse<string>();

            }
        }

        private string GenerateJwtToken(ApplicationUser user, List<string> roles)
        {
            var claims = new List<Claim>
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim("FirstName",user.FirstName),
            new Claim("LastName",user.FirstName),
            new Claim("UserName",user.UserName),
            new Claim("Email",user.Email),
        };
            var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();
            claims.AddRange(roleClaims);


            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(_configuration["JwtSettings:TokenLifetime"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
