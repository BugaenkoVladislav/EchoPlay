using App.EchoPlay.Dtos;
using App.EchoPlay.Mappers;
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
                var user  = await _authService.GetUserAsync(dto.UserData.Email, dto.UserData.Password);
                if (await _authService.CheckCorrectCode(user, dto.Code.Value))
                {
                    if (dto.IsSignUp)
                    {
                        await _authService.SignUpAsync(user);
                    }
                    
                    return Ok(user);
                }
                throw new UnauthorizedAccessException();
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("identify")]
        public async Task<IActionResult> Identify([FromBody] LoginPasswordDto dto)
        {
            try
            {
                await _authService.IdentifyUserAsync(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDto dto)
        {
            await _authService.SendCodeOnEmail(UserMappers.MapFromSignUpDto(dto));
            return Ok();
        }

        [HttpPost("unauthenticate")]
        public async Task<IActionResult> Unauthenticate([FromBody] AuthDto dto)
        {
            try
            {
                var user  = await _authService.GetUserAsync(dto.UserData.Email, dto.UserData.Password);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

