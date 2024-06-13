using AquaTrack.Services;
using AquaTrack.Services.Interfaces;
using AquaTrack.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AquaTrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthentificationService _authService;

        public UserController(IAuthentificationService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginViewModel request)
        {
            var user = await _authService.Login(request.Email, request.Password);
            if (user == null)
            {
                return BadRequest("Invalid email or password.");
            }

            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterViewModel model)
        {
            var user = await _authService.RegisterUser(model);
            if (user == null)
            {
                return BadRequest("Registration failed.");
            }

            return Ok(user);
        }

        [HttpGet("userinfo")]
        public async Task<IActionResult> GetCurrentUserInfo()
        {
            var user = await _authService.GetCurrentUserInfo();
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UserUpdateViewModel model)
        {
            var updatedUser = await _authService.UpdateUserInfo(model);
            if (updatedUser == null)
            {
                return BadRequest("Update failed.");
            }

            return Ok(updatedUser);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var result = await _authService.Logout();
            if (!result)
            {
                return BadRequest("Logout failed.");
            }

            return Ok("Logged out successfully.");
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _authService.GetAllUsers();
            if (users == null)
            {
                return NotFound("No users found.");
            }

            return Ok(users);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var result = await _authService.DeleteUser(userId);
            if (!result)
            {
                return BadRequest($"Failed to delete user with ID {userId}.");
            }

            return Ok($"User with ID {userId} deleted successfully.");
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddUser([FromBody] UserUtilityViewModel model)
        {
            var user = await _authService.AddUser(model);
            if (user == null)
            {
                return BadRequest("Failed to add user.");
            }

            return Ok(user);
        }

        [HttpPut("update/{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UserUtilityViewModel model)
        {
            var updatedUser = await _authService.UpdateUser(userId, model);
            if (updatedUser == null)
            {
                return BadRequest($"Failed to update user with ID {userId}.");
            }

            return Ok(updatedUser);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserViewModel>> GetUserById(int id)
        {
            var user = await _authService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
