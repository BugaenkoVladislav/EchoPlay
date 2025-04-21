using System.Text.Json;
using App.EchoPlay.Services;
using EchoPlayWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace EchoPlayWeb.Controllers.Authentication;

public class AuthenticationController(AuthService authService) : Controller
{
    private readonly AuthService _authService = authService;

    [HttpGet]
    public IActionResult SignIn()
    {
        return View();
    }
    [HttpGet]
    public IActionResult SignUp()
    {
        return View();
    }

    [HttpGet]
    public IActionResult TwoFactorAuth()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Identify(LoginPasswordViewModel model)
    {
        try
        {
            await _authService.IdentifyUserAsync(new Domain.EchoPlay.Entities.User()
            {
                Email = model.Email,
                Password = model.Password
            });
            TempData["LoginModel"] = JsonSerializer.Serialize(model);
            return RedirectToAction("TwoFactorAuth", "Authentication");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    #region GoogleAuth

    [HttpPost]
    public async Task LoginGoogle()
    {
        await _authService.AuthenticateAsync(new Domain.EchoPlay.Entities.User(),
            Domain.EchoPlay.Enums.AuthType.Google, 0);
    }

    [HttpPost]
    public async Task LogoutGoogle()
    {
        await _authService.UnauthenticateAsync(new Domain.EchoPlay.Entities.User());
    }

    #endregion

    #region CookieAuth

    [HttpPost]
    public async Task LoginCookie(LoginPasswordViewModel model, long code = 0)
    {
        await _authService.AuthenticateAsync(new Domain.EchoPlay.Entities.User()
        {
            Email = model.Email,
            Password = model.Password
        }, Domain.EchoPlay.Enums.AuthType.Cookie, code);
    }

    [HttpPost]
    public async Task LogoutCookie(LoginPasswordViewModel model)
    {
        await _authService.UnauthenticateAsync(new Domain.EchoPlay.Entities.User()
            { Email = model.Email, Password = model.Password });
    }

    #endregion
}