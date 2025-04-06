using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    [ResponseCache(Duration = 60)] // Cache de 60 segundos
    [Authorize]
    public IActionResult Get()
    {
        return Ok(new { message = "This is cached" });
    }
}