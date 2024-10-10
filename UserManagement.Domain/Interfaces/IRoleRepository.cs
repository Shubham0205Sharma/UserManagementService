using System;

using UserManagement.Domain.Entities;
using UserManagement.Domain.Enums;

namespace UserManagement.Domain.Interfaces
{
    public interface IRoleRepository
    {
        Task<ApplicationRole> GetRoleByIdAsync(string id);
        Task<ApplicationRole> GetRoleByNameAsync(string roleName);
        Task<List<ApplicationRole>> GetAllRolesAsync();
        Task<ApplicationRole> AddRoleAsync(string roleName);
        Task<bool> UpdateRoleAsync(ApplicationRole role);
        Task<bool> DeleteRole(ApplicationRole role);
        Task<bool> RoleExistsAsync(string roleName);
    }

}
