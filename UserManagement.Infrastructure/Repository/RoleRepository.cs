using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Interfaces;

namespace UserManagement.Infrastructure.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RoleRepository(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<ApplicationRole> AddRoleAsync(string roleName)
        {
            var result = await _roleManager.CreateAsync(new ApplicationRole { Name = roleName });
            if (result.Succeeded)
            {
                return await GetRoleByNameAsync(roleName);
            }
            return null;
        }

        public async Task<bool> DeleteRole(ApplicationRole role)
        {
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
                return true;
            return false;

        }
        public async Task<List<ApplicationRole>> GetAllRolesAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }
        public async Task<ApplicationRole> GetRoleByIdAsync(string id)
        {
            return await _roleManager.FindByIdAsync(id);

        }

        public async Task<ApplicationRole> GetRoleByNameAsync(string roleName)
        {
            return await _roleManager.FindByNameAsync(roleName);
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }

        public async Task<bool> UpdateRoleAsync(ApplicationRole role)
        {
            var result = await _roleManager.UpdateAsync(role);
            return result.Succeeded;
        }
    }
}

