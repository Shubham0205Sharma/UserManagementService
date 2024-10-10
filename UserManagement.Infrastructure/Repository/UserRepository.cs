using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Interfaces;

namespace UserManagement.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<ApplicationUser> GetUserByNameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);

        }

        public async Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            var result = await _userManager.Users.ToListAsync();
            return result;
        }
        public async Task<List<string>> GetUserRoleAsync(ApplicationUser user)
        {
            var result = await _userManager.GetRolesAsync(user);
            return result.ToList();
        }

        public async Task<List<ApplicationUser>> GetUsersInRoleAsync(string roleName)
        {
            var result = await _userManager.GetUsersInRoleAsync(roleName);
            return result.ToList();
        }

        public async Task<ApplicationUser> AddUserAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded ? user : null;
        }

        public async Task<ApplicationUser> UpdateUserAsync(ApplicationUser user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded ? user : null;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                return result.Succeeded;
            }
            return false;
        }
        public async Task<bool> AssignRoleToUserAsync(ApplicationUser user,string roleName)
        {
            var isInRole = await _userManager.IsInRoleAsync(user, roleName);
            if (!isInRole)
            {
               var result = await _userManager.AddToRoleAsync(user, roleName);
                return result.Succeeded;
            }
            return false;
        }
        public async Task<SignInResult> SignInWithPwdAsync(string username, string password, bool isPersistent = false, bool lockOutEnable = false)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, isPersistent, lockOutEnable);
            return result;
        }

        public async Task<bool> UpdatePasswordAsync(ApplicationUser user, string currentPassword, string newPassword)
        {
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            return result.Succeeded;
        }

        public async Task<bool> RemoveFromRoleAsync(ApplicationUser user, string roleName)
        {
            var isInRole = await _userManager.IsInRoleAsync(user, roleName);
            if (isInRole)
            {
                var result = await _userManager.RemoveFromRoleAsync(user, roleName);
                return result.Succeeded;
            }
            return false;
        }
    }
}
