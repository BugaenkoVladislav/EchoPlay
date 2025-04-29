using App.EchoPlay.Dtos;
using App.EchoPlay.Services;
using Domain.EchoPlay.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

namespace EchoPlayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(AuthService authService, IHttpContextAccessor accessor) : ControllerBase
    {
        private readonly AuthService _authService = authService;
        private readonly IHttpContextAccessor _httpContextAccessor = accessor;

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]AuthDto dto)
        {
            try
            {
                var user = new User()
                {
                    Email = dto.UserData.Email,
                    Password = dto.UserData.Password,
                };
                
                await _authService.AuthenticateAsync(user, dto.AuthType!.Value, dto.Code!.Value);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("identify")]
        public async Task<IActionResult> Identify([FromBody] AuthDto dto)
        {
            try
            {
                await _authService.IdentifyUserAsync(new User()
                {
                    Email = dto.UserData.Email,
                    Password = dto.UserData.Password,
                });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("unauthenticate")]
        public async Task<IActionResult> Unauthenticate([FromBody] AuthDto dto)
        {
            try
            {
                await _authService.UnauthenticateAsync(new User()
                {
                    Email = dto.UserData.Email,
                    Password = dto.UserData.Password,
                });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}