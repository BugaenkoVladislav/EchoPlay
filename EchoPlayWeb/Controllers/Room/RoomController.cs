using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EchoPlayWeb.Controllers.Room;

public class RoomController : Controller
{
    // GET
    [Route("Room/Index/{roomId}")]  
    public IActionResult Index(string roomId)
    {
        ViewData["RoomId"] = roomId;
        return View();
    }
    [Authorize]
    public IActionResult TestAuth()
    {
        return View();
    }
}