using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PostSync.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TestController : Controller
{

    [HttpGet]
    public IActionResult test()
    {
        return Ok("Test done");
    }
    
}