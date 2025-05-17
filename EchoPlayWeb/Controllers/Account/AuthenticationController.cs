using System.Security.Claims;
using System.Text.Json;
using App.EchoPlay.Dtos;
using App.EchoPlay.Fabrics;
using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Enums;
using Domain.EchoPlay.Interfaces;
using EchoPlayWeb.Models;
using Google.Apis.Auth.AspNetCore3;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace EchoPlayWeb.Controllers.Account;

public class AccountController(IHttpClientFactory httpClientFactory,AuthenticationCreator authenticationCreator) : Controller
{
    private readonly AuthenticationCreator _authenticationCreator = authenticationCreator;
    private IAuthentication<User> _authentication;
    private readonly HttpClient _authHttpClient = httpClientFactory.CreateClient("AuthApi");

    [HttpGet]
    public IActionResult Login() => View();

    [HttpGet]
    public IActionResult SignUp() => View();

    [HttpGet]
    public IActionResult TwoFactorAuth() => View();

    [HttpPost]
    public async Task<IActionResult> Identify(LoginPasswordViewModel model)
    {
        try
        {
            var authDto = new LoginPasswordDto
            {
                Email = model.Email,
                Password = model.Password
            };
            var response = await _authHttpClient.PostAsJsonAsync("api/Authentication/identify", authDto);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }

            TempData["LoginModel"] = JsonSerializer.Serialize(model);
            TempData["IsSignUp"] = false;
            return RedirectToAction("TwoFactorAuth", "Account");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View("Login", model);
        }
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpViewModel model)
    {
        try
        {
            var signUpDto = new SignUpDto()
            {
                Email = model.Email,
                Password = model.Password,
                Username = model.Username
            };
            var response = await _authHttpClient.PostAsJsonAsync("api/Authentication/signup", signUpDto);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
            TempData["LoginModel"] = JsonSerializer.Serialize(model);
            TempData["IsSignUp"] = true;
            return RedirectToAction("TwoFactorAuth", "Account");
            
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View("SignUp", model);
        }
    }

    #region CookieAuth

    [HttpPost]
    public async Task<IActionResult> LoginCookie(LoginPasswordViewModel model, bool isSignUp,long code = 0)
    {
        try
        {
            var authDto = new AuthDto
            {
                UserData = new LoginPasswordDto()
                {
                    Email = model.Email,
                    Password = model.Password
                }, 
                AuthType = AuthType.Cookie,
                Code = code,
                IsSignUp = isSignUp
            };
            
            var response = await _authHttpClient.PostAsJsonAsync("api/Authentication/authenticate", authDto);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
            var stringResponse = await response.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<User>(stringResponse,new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            _authentication = _authenticationCreator.Create(AuthType.Cookie);
            await _authentication.AuthenticateAsync(user);
            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View("Login", model);
        }
    }

    [HttpPost]
    public async Task<IActionResult> LogoutCookie(LoginPasswordViewModel model)
    {
        try
        {
            var response = await _authHttpClient.PostAsJsonAsync("api/Authentication/unauthenticate", new AuthDto()
            {
                UserData = new LoginPasswordDto()
                {
                    Email = model.Email,
                    Password = model.Password
                }
            });
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
            return RedirectToAction("Login");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    #endregion
    
    #region GoogleAuth

    [HttpPost]
    public async Task<IActionResult> LoginGoogle()
    {
        try
        {
            var authProperties = new AuthenticationProperties
            {
                RedirectUri = "/Account/GoogleCallback" 
            };

            return Challenge(authProperties, GoogleOpenIdConnectDefaults.AuthenticationScheme);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    } 
    
    [HttpGet]
    public async Task<IActionResult> GoogleCallback()
    {
        
        var result = await HttpContext.AuthenticateAsync(GoogleOpenIdConnectDefaults.AuthenticationScheme);
    
        if (!result.Succeeded)
            return BadRequest("Google login failed.");

        var claims = result.Principal.Claims;
        IDictionary<string,string> claimsDict = new Dictionary<string,string>();
        var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        var username = claims.FirstOrDefault(c => c.Type == "name")?.Value;
        
        var user = new User()
        {
            Email = email,
            Username = username
        };
        
        _authentication = _authenticationCreator.Create(AuthType.Cookie);
        await _authentication.AuthenticateAsync(user);
        
        return RedirectToAction("Index", "Home");
    }

    
    #endregion
}