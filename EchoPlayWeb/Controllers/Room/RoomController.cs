using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EchoPlayWeb.Controllers.Room;
[Authorize(AuthenticationSchemes = "CookieScheme")]
public class RoomController : Controller
{
    [Route("Room/Index/{roomId}")]  
    public IActionResult Index(string roomId)
    {
        ViewData["RoomId"] = roomId;
        return View();
    }
}