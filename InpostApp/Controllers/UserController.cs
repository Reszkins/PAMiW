using InpostApp.DataAccess.Services;
using InpostApp.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InpostApp.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("identify")]
        [Authorize]
        public async Task<IActionResult> IdentifyUser([FromBody] IdentifyDto dto)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier) is null) return BadRequest("Invalid user");

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await _userService.HandleUser(dto.Username, userId);

            return Ok();
        }
    }
}
