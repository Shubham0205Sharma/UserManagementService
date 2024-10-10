
using UserManagement.Application.DTOs;
using UserManagement.Application.Responses;

namespace UserManagement.Application.Interfaces
{
    public interface IAdminService
    {
        Task<ApiResponse<List<UserProfileDto>>> GetAllUsersAsync();
        Task<ApiResponse<bool>> DeleteUserAsync(string userId);
        Task<ApiResponse<RoleDto>> AddRoleAsync(string roleName);
        Task<ApiResponse<bool>> RemoveRoleAsync(string roleName);
        Task<ApiResponse<RoleDto>> GetRoleByNameAsync(string roleName);
        Task<ApiResponse<List<RoleDto>>> GetAllRolesAsync();
        Task<ApiResponse<bool>> UpdateRoleAsync(UpdateRoleDto roleDto);
        Task<ApiResponse<bool>> AssignRoleToUserAsync(string userId, string roleName);
        Task<ApiResponse<bool>> RemoveRoleAssignmentAsync(string userId, string roleName);
        Task<ApiResponse<List<UserProfileDto>>> GetUsersInRoleAsync(string roleName);
        Task<ApiResponse<bool>> UpdateRoleAssignmentAsync(string userId, string oldRoleName, string newRoleName);
    }
}
