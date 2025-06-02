using App.EchoPlay.Dtos;
using App.EchoPlay.Services;
using Domain.EchoPlay.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EchoPlayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(UserService userService) : ControllerBase
    {
        private readonly UserService _userService = userService;

        [HttpPost("get-photo")]
        public async Task<string> GetPhotoAsync([FromQuery]string userId)
        {
            userId = userId.Trim('"', ' ', '\t', '\n', '\r');
            var guid = Guid.Parse(userId);
            var photo = await _userService.GetUserPhoto(guid);
            return photo;
        }

        [HttpGet("get-user-id/{username}")]
        public async Task<Guid> GetUserId(string username)
        {
            return await _userService.GetUserId(username);
        }

        [HttpPut("update-profile")]
        public async Task UpdateUser([FromBody] User user)
        {
            await _userService.UpdateUser(user);
        }
        
    }
}
