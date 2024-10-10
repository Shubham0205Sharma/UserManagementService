using AutoMapper;
using FluentValidation;
using UserManagement.Application.DTOs;
using UserManagement.Common.Exceptions;
using UserManagement.Application.Extensions;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Responses;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Enums;
using UserManagement.Domain.Interfaces;

namespace UserManagement.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<RegistrationDto> _userRegistrationValidator;
        private readonly IValidator<UserDto> _userDtoValidator;
        private readonly IValidator<PasswordUpdateDto> _pwdDtoValidator;



        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<RegistrationDto> userRegistrationValidator, IValidator<UserDto> userDtoValidator, IValidator<PasswordUpdateDto> pwdDtoValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRegistrationValidator = userRegistrationValidator;
            _userDtoValidator = userDtoValidator;
            _pwdDtoValidator = pwdDtoValidator;
        }

        public async Task<ApiResponse<UserProfileDto>> AddUserAsync(RegistrationDto registrationDto)
        {
            try
            {
                var validationResult = registrationDto.Validate(_userRegistrationValidator);
                if (!validationResult.IsValid)
                {
                    return validationResult.ToApiResponse<UserProfileDto>("Validation failed");
                }

                var user = _mapper.Map<ApplicationUser>(registrationDto);
                var result = await _unitOfWork.Users.AddUserAsync(user, registrationDto.Password);
                if (result == null)
                {
                    throw new CustomAppException("Failed to register user");
                }

                var isRoleAssigned = string.IsNullOrEmpty(RoleType.User.ToString()) ? false : await _unitOfWork.Users.AssignRoleToUserAsync(result, RoleType.User.ToString());
                var userProfile = _mapper.Map<UserProfileDto>(result);
                userProfile.Roles = isRoleAssigned ? new List<string> { RoleType.User.ToString() } : null;
                return new ApiResponse<UserProfileDto>(true, "User registered successfully", userProfile);
            }
            catch (CustomAppException cex)
            {
                return cex.ToApiResponse<UserProfileDto>();
            }
            catch (Exception ex)
            {
                return ex.ToApiResponse<UserProfileDto>();
            }
        }

        public async Task<ApiResponse<UserProfileDto>> GetUserByIdAsync(string userId)
        {
            try
            {
                var user = await _unitOfWork.Users.GetUserByIdAsync(userId);
                if (user == null)
                    throw new CustomAppException("No user found");
                var userDetails = _mapper.Map<UserProfileDto>(user);
                var roles = await _unitOfWork.Users.GetUserRoleAsync(user);
                userDetails.Roles = roles.Count > 0 ? roles : null;
                return new ApiResponse<UserProfileDto>(true, "User details found.", userDetails);
            }
            catch (CustomAppException cex)
            {
                return cex.ToApiResponse<UserProfileDto>();
            }
            catch (Exception ex)
            {
                return ex.ToApiResponse<UserProfileDto>();
            }
        }

        public async Task<ApiResponse<UserProfileDto>> GetUserByNameAsync(string userName)
        {
            try
            {
                var user = await _unitOfWork.Users.GetUserByNameAsync(userName);
                if (user == null)
                    throw new CustomAppException("No user found");
                var userDetails = _mapper.Map<UserProfileDto>(user);
                var roles = await _unitOfWork.Users.GetUserRoleAsync(user);
                userDetails.Roles = roles.Count > 0 ? roles : null;
                return new ApiResponse<UserProfileDto>(true, "User details found.", userDetails);
            }
            catch (CustomAppException cex)
            {
                return cex.ToApiResponse<UserProfileDto>();
            }
            catch (Exception ex)
            {
                return ex.ToApiResponse<UserProfileDto>();
            }
        }

        public async Task<ApiResponse<UserProfileDto>> GetUserByEmailAsync(string email)
        {
            try
            {
                var user = await _unitOfWork.Users.GetUserByEmailAsync(email);
                if (user == null)
                    throw new CustomAppException("No user found");
                var userDetails = _mapper.Map<UserProfileDto>(user);
                var roles = await _unitOfWork.Users.GetUserRoleAsync(user);
                userDetails.Roles = roles.Count > 0 ? roles : null;
                return new ApiResponse<UserProfileDto>(true, "User details found.", userDetails);
            }
            catch (CustomAppException cex)
            {
                return cex.ToApiResponse<UserProfileDto>();
            }
            catch (Exception ex)
            {
                return ex.ToApiResponse<UserProfileDto>();
            }
        }

        public async Task<ApiResponse<UserProfileDto>> UpdateUserAsync(UserDto userDto)
        {
            try
            {
                var validationResult = userDto.Validate(_userDtoValidator);
                if (!validationResult.IsValid)
                {
                    return validationResult.ToApiResponse<UserProfileDto>("Validation failed");
                }

               var user = await _unitOfWork.Users.GetUserByIdAsync(userDto.Id);
                if (user == null)
                    throw new CustomAppException("No user found");
                _mapper.Map(userDto, user);
                var result = await _unitOfWork.Users.UpdateUserAsync(user);
                if (result == null)
                {
                    throw new CustomAppException("Failed to register user");
                }
                var userProfile = _mapper.Map<UserProfileDto>(result);
                return new ApiResponse<UserProfileDto>(true, "User updated successfully", userProfile);


            }
            catch (CustomAppException cex)
            {
                return cex.ToApiResponse<UserProfileDto>();
            }
            catch (Exception ex)
            {
                return ex.ToApiResponse<UserProfileDto>();
            }
        }

        public async Task<ApiResponse<bool>> UpdatePasswordAsync(PasswordUpdateDto passwordDto)
        {
            try
            {
                var validationResult = passwordDto.Validate(_pwdDtoValidator);
                if (!validationResult.IsValid)
                {
                    return validationResult.ToApiResponse<bool>("Validation failed");
                }

                var user = await _unitOfWork.Users.GetUserByIdAsync(passwordDto.UserId);
                if (user == null)
                    throw new CustomAppException("No user found.");

                var result = await _unitOfWork.Users.UpdatePasswordAsync(user, passwordDto.CurrentPassword, passwordDto.NewPassword);
                if(!result)
                    throw new CustomAppException("Failed to update password.");

                return new ApiResponse<bool>(true, "Password successfully changed.", result);
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
