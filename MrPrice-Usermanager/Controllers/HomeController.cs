using Microsoft.AspNetCore.Mvc;


namespace MrPrice_Usermanager.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return Ok();
    }

    public IActionResult Privacy()
    {
        return Ok();
    }

   
}