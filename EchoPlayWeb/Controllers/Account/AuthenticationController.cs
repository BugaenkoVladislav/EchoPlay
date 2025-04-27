using System.Text.Json;
using App.EchoPlay.Dtos;
using App.EchoPlay.Services;
using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Enums;
using EchoPlayWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace EchoPlayWeb.Controllers.Account;

public class AccountController(IHttpClientFactory httpClientFactory) : Controller
{
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
            var authDto = new AuthDto
            {
                UserData = new LoginPasswordDto
                {
                    Email = model.Email,
                    Password = model.Password
                }
            };
            var requestUri = new Uri(_authHttpClient.BaseAddress, "api/Authentication/identify");
            
            Console.WriteLine($"Request URI: {requestUri}");
            var response = await _authHttpClient.PostAsJsonAsync("api/Authentication/identify", authDto);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }

            TempData["LoginModel"] = JsonSerializer.Serialize(model);
            return RedirectToAction("TwoFactorAuth", "Account");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View("Login", model);
        }
    }

    #region CookieAuth

    [HttpPost]
    public async Task<IActionResult> LoginCookie(LoginPasswordViewModel model, long code = 0)
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
                Code = code
            };
            var response = await _authHttpClient.PostAsJsonAsync("api/Authentication/authenticate", authDto);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }

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
}