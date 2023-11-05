using Integration.Core.Interfaces.OAuth;
using Microsoft.AspNetCore.Mvc;

namespace Integration.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : Controller
{
    private IFacebookOAuth _facebookOAuth;

    public TestController(IFacebookOAuth facebookOAuth)
    {
        _facebookOAuth = facebookOAuth;
    }
    
    [HttpGet]
    public IActionResult Test()
    {
        var url = _facebookOAuth.GetOAuthUrl();
        return Ok(url);
    }
}