using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web_api.Services;

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

    [HttpGet("admin-only")]
    [Permission("Admin")] // Only users with 'Admin' permission level can access this endpoint
    public IActionResult GetAdminData()
    {
        return Ok(new { message = "This is admin-only data." });
    }
}