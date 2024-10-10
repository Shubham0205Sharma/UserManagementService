using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.DTOs;
using UserManagement.Application.Interfaces;

namespace UserManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("getallusers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _adminService.GetAllUsersAsync();
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpGet("getallroles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await _adminService.GetAllRolesAsync();
            return result.Success ? Ok(result) : NotFound(result);
        }
        [HttpGet("getrole/{roleName}")]
        public async Task<IActionResult> GetRoleByName(string roleName)
        {
            var result = await _adminService.GetRoleByNameAsync(roleName);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpGet("usersbyrole/{roleName}")]
        public async Task<IActionResult> GetUsersInRole(string roleName)
        {
            var result = await _adminService.GetUsersInRoleAsync(roleName);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpPost("addrole/{roleName}")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            var result = await _adminService.AddRoleAsync(roleName);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("assignrole")]
        public async Task<IActionResult> AssignRoleToUser(string userId, string roleName)
        {
            var result = await _adminService.AssignRoleToUserAsync(userId, roleName);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("update-roleassignment")]
        public async Task<IActionResult> UpdateRoleAssignment(string userId, string oldRoleName, string newRoleName)
        {
            var result = await _adminService.UpdateRoleAssignmentAsync(userId, oldRoleName, newRoleName);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("updaterole")]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleDto role)
        {
            var result = await _adminService.UpdateRoleAsync(role);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("remove-role/{roleName}")]
        public async Task<IActionResult> DeleteRole(string roleName)
        {
            var result = await _adminService.RemoveRoleAsync(roleName);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("remove-roleassignment")]
        public async Task<IActionResult> RemoveRoleAssignment(string userId, string roleName)
        {
            var result = await _adminService.RemoveRoleAssignmentAsync(userId, roleName);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("remove-user/{userId}")]
        public async Task<IActionResult> RemoveUser(string userId)
        {
            var result = await _adminService.DeleteUserAsync(userId);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
