using AutoMapper;
using FluentValidation;
using UserManagement.Application.DTOs;
using UserManagement.Common.Exceptions;
using UserManagement.Application.Extensions;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Responses;
using UserManagement.Domain.Interfaces;

namespace UserManagement.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateRoleDto> _updateRoleDtoValidator;


        public AdminService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateRoleDto> updateRoleDtoValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _updateRoleDtoValidator = updateRoleDtoValidator;
        }

        public async Task<ApiResponse<RoleDto>> AddRoleAsync(string roleName)
        {
            try
            {
                var result = await _unitOfWork.Roles.AddRoleAsync(roleName);
                if (result == null)
                    throw new CustomAppException($"Failed to add {roleName} role.");
                var role = _mapper.Map<RoleDto>(result);
                return new ApiResponse<RoleDto>(true, $"{roleName} role Added.", role);
            }
            catch (CustomAppException cex)
            {
                return cex.ToApiResponse<RoleDto>();
            }
            catch (Exception ex)
            {
                return ex.ToApiResponse<RoleDto>();
            }
        }

        public async Task<ApiResponse<bool>> RemoveRoleAsync(string roleName)
        {
            try
            {
                var role = await _unitOfWork.Roles.GetRoleByNameAsync(roleName);
                if (role == null)
                    throw new CustomAppException($"{roleName} role not found.");

                var result = await _unitOfWork.Roles.DeleteRole(role);
                if (!result)
                    throw new CustomAppException($"Failed to delete {roleName} role");
                return new ApiResponse<bool>(true, $" {roleName} role deleted successfully", result);
            }
            catch (CustomAppException cex)
            {
                return cex.ToApiResponse<bool>();
            }
            catch (Exception ex)
            {
                return ex.ToApiResponse<bool>();
            }

        }

        public async Task<ApiResponse<RoleDto>> GetRoleByNameAsync(string roleName)
        {
            try
            {
                var result = await _unitOfWork.Roles.GetRoleByNameAsync(roleName);
                if (result == null)
                    throw new CustomAppException($"{roleName} role not found.");
                var role = _mapper.Map<RoleDto>(result);
                return new ApiResponse<RoleDto>(true, $"{roleName} role found.", role);
            }
            catch (CustomAppException cex)
            {
                return cex.ToApiResponse<RoleDto>();
            }
            catch (Exception ex)
            {
                return ex.ToApiResponse<RoleDto>();
            }
        }

        public async Task<ApiResponse<List<RoleDto>>> GetAllRolesAsync()
        {
            try
            {
                var roles = await _unitOfWork.Roles.GetAllRolesAsync();
                if (roles.Count < 0)
                    throw new CustomAppException("No roles found.");
                var listofRoles = _mapper.Map<List<RoleDto>>(roles);
                return new ApiResponse<List<RoleDto>>(true, "Roles found successfully.", listofRoles);
            }
            catch (CustomAppException cex)
            {
                return cex.ToApiResponse<List<RoleDto>>();
            }
            catch (Exception ex)
            {
                return ex.ToApiResponse<List<RoleDto>>();
            }
        }

        public async Task<ApiResponse<bool>> UpdateRoleAsync(UpdateRoleDto roleDto)
        {
            try
            {
                var validationResult = roleDto.Validate(_updateRoleDtoValidator);
                if (!validationResult.IsValid)
                {
                    return validationResult.ToApiResponse<bool>("Validation failed");
                }

                var ifRoleExist = await _unitOfWork.Roles.RoleExistsAsync(roleDto.OldRoleName);
                if (!ifRoleExist)
                    throw new CustomAppException($"No Role Found with name {roleDto.OldRoleName} to update. Please create a new role.");
                var role = await _unitOfWork.Roles.GetRoleByNameAsync(roleDto.OldRoleName);
                role.Name = roleDto.NewRoleName;
                var result = await _unitOfWork.Roles.UpdateRoleAsync(role);
                return new ApiResponse<bool>(true, "Role updated successfully.", result);
            }
            catch (CustomAppException cex)
            {
                return cex.ToApiResponse<bool>();
            }
            catch (Exception ex)
            {
                return ex.ToApiResponse<bool>();
            }

        }
        public async Task<ApiResponse<List<UserProfileDto>>> GetAllUsersAsync()
        {
            try
            {
                var result = await _unitOfWork.Users.GetAllUsersAsync();
                if (result.Count < 0)
                    throw new CustomAppException("No users found.");

                var users = new List<UserProfileDto>();

                foreach (var user in result)
                {
                    var userProfileDto = _mapper.Map<UserProfileDto>(user);
                    var roles = await _unitOfWork.Users.GetUserRoleAsync(user);
                    userProfileDto.Roles = roles.ToList();
                    users.Add(userProfileDto);
                }
                return new ApiResponse<List<UserProfileDto>>(true, "Users found successfully.", users);
            }
            catch (CustomAppException cex)
            {
                return cex.ToApiResponse<List<UserProfileDto>>();
            }
            catch (Exception ex)
            {
                return ex.ToApiResponse<List<UserProfileDto>>();
            }
        }

        public async Task<ApiResponse<bool>> DeleteUserAsync(string userId)
        {
            try
            {
                var result = await _unitOfWork.Users.DeleteUserAsync(userId);
                if (!result)
                    throw new CustomAppException("Failed to delete user");
                return new ApiResponse<bool>(true, "User deleted successfully", result);
            }
            catch (CustomAppException cex)
            {
                return cex.ToApiResponse<bool>();
            }
            catch (Exception ex)
            {
                return ex.ToApiResponse<bool>();
            }

        }

        public async Task<ApiResponse<bool>> AssignRoleToUserAsync(string userId, string roleName)
        {
            try
            {
                var user = await _unitOfWork.Users.GetUserByIdAsync(userId);
                if (user == null)
                    throw new CustomAppException($"No users found for given userid {userId}.");
                if (!await _unitOfWork.Roles.RoleExistsAsync(roleName))
                    throw new CustomAppException($"No roles found for given role {roleName}.");

                var result = await _unitOfWork.Users.AssignRoleToUserAsync(user, roleName);
                if (!result)
                    throw new CustomAppException($"Failed to assign role for user {user.UserName}.");
                return new ApiResponse<bool>(true, $"Role assigned successfully for user: {user.UserName}.", result);
            }
            catch (CustomAppException cex)
            {
                return cex.ToApiResponse<bool>();
            }
            catch (Exception ex)
            {
                return ex.ToApiResponse<bool>();
            }

        }

        public async Task<ApiResponse<bool>> RemoveRoleAssignmentAsync(string userId, string roleName)
        {
            try
            {
                var user = await _unitOfWork.Users.GetUserByIdAsync(userId);
                if (user == null)
                    throw new CustomAppException($"No users found for given userid {userId}.");
                if (!await _unitOfWork.Roles.RoleExistsAsync(roleName))
                    throw new CustomAppException($"No roles found for given role {roleName}.");

                var result = await _unitOfWork.Users.RemoveFromRoleAsync(user, roleName);
                if (!result)
                    throw new CustomAppException($"Failed To remove role for user {user.UserName}.");
                return new ApiResponse<bool>(true, $"Role removed successfully for user: {user.UserName}.", result);
            }
            catch (CustomAppException cex)
            {
                return cex.ToApiResponse<bool>();
            }
            catch (Exception ex)
            {
                return ex.ToApiResponse<bool>();
            }
        }

        public async Task<ApiResponse<List<UserProfileDto>>> GetUsersInRoleAsync(string roleName)
        {
            try
            {
                var result = await _unitOfWork.Users.GetUsersInRoleAsync(roleName);
                if (result.Count < 0)
                    throw new CustomAppException("No users found.");
                var users = _mapper.Map<List<UserProfileDto>>(result);

                return new ApiResponse<List<UserProfileDto>>(true, "Users found successfully.", users);
            }
            catch (CustomAppException cex)
            {
                return cex.ToApiResponse<List<UserProfileDto>>();
            }
            catch (Exception ex)
            {
                return ex.ToApiResponse<List<UserProfileDto>>();
            }
        }

        public async Task<ApiResponse<bool>> UpdateRoleAssignmentAsync(string userId, string oldRoleName, string newRoleName)
        {
            try
            {
                var user = await _unitOfWork.Users.GetUserByIdAsync(userId);
                if (user == null)
                    throw new CustomAppException("No users found.");
                if (!await _unitOfWork.Roles.RoleExistsAsync(oldRoleName))
                    throw new CustomAppException("No previou roles found.");

                var isRemoved = await _unitOfWork.Users.RemoveFromRoleAsync(user, oldRoleName);
                if (!isRemoved)
                    throw new CustomAppException("Failed To Remove Role.");
                var result = await _unitOfWork.Users.AssignRoleToUserAsync(user, newRoleName);
                if (!result)
                    throw new CustomAppException("Failed To Update Role.");

                return new ApiResponse<bool>(true, $"Role update successfully for user: {user.UserName}.", result);
            }
            catch (CustomAppException cex)
            {
                return cex.ToApiResponse<bool>();
            }
            catch (Exception ex)
            {
                return ex.ToApiResponse<bool>();
            }
        }

    }
}
