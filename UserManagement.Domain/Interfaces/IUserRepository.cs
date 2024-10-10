using Microsoft.AspNetCore.Identity;
using UserManagement.Domain.Entities;

namespace UserManagement.Domain.Interfaces
{

    public interface IUserRepository
    {
        Task<ApplicationUser> GetUserByIdAsync(string id);
        Task<ApplicationUser> GetUserByNameAsync(string name);
        Task<ApplicationUser> GetUserByEmailAsync(string name);
        Task<List<ApplicationUser>> GetAllUsersAsync();
        Task<List<string>> GetUserRoleAsync(ApplicationUser user);
        Task<ApplicationUser> AddUserAsync(ApplicationUser user,string password);
        Task<ApplicationUser> UpdateUserAsync(ApplicationUser user);
        Task<bool> DeleteUserAsync(string userId);
        Task<bool> AssignRoleToUserAsync(ApplicationUser user, string roleName);
        Task<bool> RemoveFromRoleAsync(ApplicationUser user, string roleName);
        Task<SignInResult> SignInWithPwdAsync(string username, string password, bool isPersistent = false, bool lockOutEnable =false);
        Task<bool> UpdatePasswordAsync(ApplicationUser user, string currentPassword, string newPassword);
        Task<List<ApplicationUser>> GetUsersInRoleAsync(string roleName);

    }


}
