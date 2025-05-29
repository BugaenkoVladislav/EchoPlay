using System.Text;
using App.EchoPlay.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EchoPlayWeb.Controllers.Room;
[Authorize(AuthenticationSchemes = "CookieScheme")]
public class RoomController(IHttpClientFactory httpClientFactory) : Controller
{
    private readonly HttpClient _userHttpClient = httpClientFactory.CreateClient("Api");
    
    [Route("Room/Index/{roomId}")]  
    public IActionResult Index(string roomId)
    {
        ViewData["RoomId"] = roomId;
        return View();
    }
    
    public async Task<Uri> GetUserPhoto(string username)
    {
        // 1. Получаем userId
        var response = await _userHttpClient.GetAsync($"api/User/get-user-id/{username}");
        response.EnsureSuccessStatusCode(); // Проверяем успешность запроса
        var userId = await response.Content.ReadAsStringAsync();

        // 2. Отправляем запрос с userId в query-параметре
        var responsePhoto = await _userHttpClient.PostAsync($"api/User/get-photo?userId={Uri.EscapeDataString(userId)}", null);
        responsePhoto.EnsureSuccessStatusCode(); // Проверяем успешность
    
        // 3. Возвращаем URL фото
        var photoUrl = await responsePhoto.Content.ReadAsStringAsync();
        return new Uri(photoUrl);
    }
    
    public async Task AddUserPhoto(string username,string photo)
    {
        var response = await _userHttpClient.GetAsync($"api/User/get-user-id/{username}");
        var userId = await response.Content.ReadAsStringAsync();
        var usernamePhoto = new UsernamePhotoDto()
        {
            PhotoPath = photo,
            UserId = Guid.Parse(userId)
        };
        var json = JsonConvert.SerializeObject(usernamePhoto);
        var content = new StringContent(json);
        await _userHttpClient.PostAsync("api/User/add-photo",content);
    }
    
}