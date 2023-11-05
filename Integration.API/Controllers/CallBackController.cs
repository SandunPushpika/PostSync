using Microsoft.AspNetCore.Mvc;

namespace Integration.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CallBackController : Controller
{
    [HttpGet("facebook")]
    public IActionResult FacebookRedirect()
    {
        var http = Request;
        return Ok();
    }
}