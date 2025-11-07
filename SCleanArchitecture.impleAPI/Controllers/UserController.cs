using Microsoft.AspNetCore.Mvc;
using SCleanArchitecture.SimpleAPI.Application.DTOs;
using SCleanArchitecture.SimpleAPI.Application.Services;

namespace SCleanArchitecture.SimpleAPI.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        //private readonly UserService userService1;
        public UserController(IUserService userService)
        {
          _userService = userService;   
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserRequestDto userRequestDto)
        {
            var result = await _userService.AddUser(userRequestDto);
            if(result == null)
            {
                return BadRequest("Invalid user data.");
            }

            return Ok(result);
        }
        
        [HttpGet("id")]
        public async Task<IActionResult> GetUser(int id)
        {
            var result = "user data";

            return Ok(result);
        }
    }
}
