using UserManagement.Application.DTOs;
using UserManagement.Application.Responses;

namespace UserManagement.Application.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<string>> LoginAsync(LoginDto loginDto);
    }
}