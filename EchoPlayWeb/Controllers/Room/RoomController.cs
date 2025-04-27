using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EchoPlayWeb.Controllers.Room;

public class RoomController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
    [Authorize]
    public IActionResult TestAuth()
    {
        return View();
    }
}