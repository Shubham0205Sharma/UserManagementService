using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.DTOs;
using UserManagement.Application.Interfaces;

namespace UserManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
                _userService = userService;
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var result = await _userService.GetUserByEmailAsync(email);
            return result.Success ? Ok(result) : NotFound(result);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            return result.Success ? Ok(result) : NotFound(result);

        }

        [HttpGet("username/{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var result = await _userService.GetUserByNameAsync(username);
            return result.Success ? Ok(result) : NotFound(result);

        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody] RegistrationDto user)
        {
            var result = await _userService.AddUserAsync(user);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto user)
        {
            var result = await _userService.UpdateUserAsync(user);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] PasswordUpdateDto passwordUpdateDto)
        {
            var result = await _userService.UpdatePasswordAsync(passwordUpdateDto);
            return result.Success ? Ok(result) : BadRequest(result);
        }

    }
}
