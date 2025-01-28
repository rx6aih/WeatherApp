using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WeatherApp.API.Controllers;
[ApiController]
[Route("Test")]
public class Test : ControllerBase
{
    [HttpGet("Testing")]
    [Authorize]
    public async Task<IActionResult> Testo()
    {
        return Ok();
    }
}