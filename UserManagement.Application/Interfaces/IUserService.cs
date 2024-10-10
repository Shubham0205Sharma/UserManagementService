
using UserManagement.Application.DTOs;
using UserManagement.Application.Responses;

namespace UserManagement.Application.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponse<UserProfileDto>> AddUserAsync(RegistrationDto userDto);
        Task<ApiResponse<UserProfileDto>> GetUserByIdAsync(string userId);
        Task<ApiResponse<UserProfileDto>> GetUserByNameAsync(string userName);
        Task<ApiResponse<UserProfileDto>> GetUserByEmailAsync(string email);
        Task<ApiResponse<UserProfileDto>> UpdateUserAsync(UserDto userDto);
        Task<ApiResponse<bool>> UpdatePasswordAsync(PasswordUpdateDto userId);

    }
}
