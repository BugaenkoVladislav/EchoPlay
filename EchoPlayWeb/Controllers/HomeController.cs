using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EchoPlayWeb.Models;
using Microsoft.AspNetCore.Authorization;

namespace EchoPlayWeb.Controllers;
[Authorize(AuthenticationSchemes = "CookieScheme")]
public class HomeController(ILogger<HomeController> logger) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
    [HttpPost]
    public IActionResult JoinRoom(string roomId)
    {
        if (string.IsNullOrEmpty(roomId))
        {
            return RedirectToAction("Index", "Home");
        }
        
        return RedirectToAction("Index", "Room", new { roomId });
    }
}