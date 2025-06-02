using System.Security.Claims;
using App.EchoPlay.Dtos;
using Domain.EchoPlay.Entities;
using EchoPlayWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data;

namespace EchoPlayWeb.Controllers.Account;
[Authorize(AuthenticationSchemes = "CookieScheme")]
public class ProfileController(IHttpClientFactory httpClientFactory) : Controller
{
    private readonly HttpClient _authHttpClient = httpClientFactory.CreateClient("Api");
    [HttpGet]
    public IActionResult Index()
    {
        var username = User.FindFirst(ClaimTypes.Name)?.Value;
        var photo = User.FindFirst(ClaimTypes.Actor)?.Value;

        var model = new ProfileViewModel
        {
            Username = username,
            UrlPhoto = photo
        };

        return View("~/Views/Account/Profile.cshtml", model);
    }

    
    [HttpPost]
    public async Task<IActionResult> UpdateProfile(ProfileViewModel model)
    {
        if (model.NewPassword == model.ConfirmNewPassword)
        {
            var authDto = new LoginPasswordDto
            {
                Email = model.OldUsername,
                Password = model.OldPassword
            };
            var response = await _authHttpClient.PostAsJsonAsync("api/Authentication/get-user", authDto);
            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadFromJsonAsync<User>();
                
                user.PhotoPath = model.UrlPhoto;
                user.Password = model.OldPassword;
                user.Username = model.OldUsername;
                
                if (model.OldUsername != model.Username)
                {
                    user.Username = model.Username;
                }
                
                if (!string.IsNullOrEmpty(model.NewPassword))
                {
                    user.Password = model.NewPassword;
                }
                
                var result =  await _authHttpClient.PutAsJsonAsync("api/User/update-profile", user);
               if (result.IsSuccessStatusCode)
               {
                   return RedirectToAction("LogoutCookie","Account");
               }
               return BadRequest("Такое имя пользователя уже есть в Базе");
                
            }
            return BadRequest("Не правильный пароль");
        }
        return BadRequest("Пароли не совпадают");
    }

}
public class ProfileViewModel
{
    public string OldUsername { get; set; }
    public string Username { get; set; }
    public string UrlPhoto { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmNewPassword { get; set; }
}