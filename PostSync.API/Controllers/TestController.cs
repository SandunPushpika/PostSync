using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;

namespace PostSync.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : Controller
{

    private readonly IBus _bus;

    public TestController(IBus bus)
    {
        _bus = bus;
    }

    [HttpGet]
    public async Task<IActionResult> test()
    {
        await _bus.Publish<TestModel>(new TestModel()
        {
            Id = 12,
            Password = "asd",
            LastName = "sada"
        });
        return Ok("Test done");
    }
    
}