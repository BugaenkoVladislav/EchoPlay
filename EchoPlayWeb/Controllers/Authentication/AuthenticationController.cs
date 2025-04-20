using EchoPlayWeb.Models;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;

namespace EchoPlayWeb.Controllers.Authentication;

public class AuthenticationController : Controller
{
    // GET
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(LoginPasswordModel model)
    {
        var httpHandler = new HttpClientHandler {
            ServerCertificateCustomValidationCallback = 
                (message, cert, chain, errors) => true // Принимаем любой сертификат
        };
        var authChannel = GrpcChannel.ForAddress("https://localhost:7255", new GrpcChannelOptions {
            HttpHandler = httpHandler
        });
        var auth = new Authenticator.AuthenticatorClient(authChannel);
        var result = await auth.IdentifyAsync(new User()
        {
            Email = model.Login,
            Password = model.Password
        });
        return new JsonResult(result);
    }
}