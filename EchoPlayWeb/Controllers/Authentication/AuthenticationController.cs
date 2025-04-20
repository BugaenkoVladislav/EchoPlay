using App.EchoPlay.Services;
using EchoPlayWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace EchoPlayWeb.Controllers.Authentication;

public class AuthenticationController(AuthService authService) : Controller
{
    private readonly AuthService _authService = authService;

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task Identify(LoginPasswordModel model)
    {
        await _authService.IdentifyUserAsync(new Domain.EchoPlay.Entities.User()
        {
            Email = model.Login,
            Password = model.Password
        });
        //await _authService.AuthenticateAsync(,Domain.EchoPlay.Enums.AuthType.Cookie,);
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
    public async Task LoginCookie(LoginPasswordModel model, long code = 0)
    {
        await _authService.AuthenticateAsync(new Domain.EchoPlay.Entities.User()
        {
            Email = model.Login,
            Password = model.Password
        }, Domain.EchoPlay.Enums.AuthType.Cookie, code);
    }

    [HttpPost]
    public async Task LogoutCookie(LoginPasswordModel model)
    {
        await _authService.UnauthenticateAsync(new Domain.EchoPlay.Entities.User()
            { Email = model.Login, Password = model.Password });
    }

    #endregion
}