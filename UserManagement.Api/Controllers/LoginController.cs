using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.DTOs;
using UserManagement.Application.Interfaces;

namespace UserManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;


        public LoginController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]LoginDto loginDto)
        {
            var result =await _authService.LoginAsync(loginDto);
            return result.Success ? Ok(result) : new UnauthorizedObjectResult(result);
        }

    }
}
