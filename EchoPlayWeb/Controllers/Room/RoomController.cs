using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EchoPlayWeb.Controllers.Room;
[Authorize]
public class RoomController : Controller
{
    public string Username  = "EmptyUser";
    // GET
    [Route("Room/Index/{roomId}")]  
    public IActionResult Index(string roomId)
    {
        Username = User.FindFirst(ClaimTypes.Name)?.Value;
        ViewData["RoomId"] = roomId;
        return View();
    }
}