using Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using ViewModel.User;

namespace ApiTesteKognit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
             _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserViewModel userViewModel)
        {
            var result = await _userService.CreateUserAsync(userViewModel);

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userService.GetAllUsersAsync();

            if(result == null || !result.Any())
                return NoContent();

            return Ok(result);
        }
    }
}
