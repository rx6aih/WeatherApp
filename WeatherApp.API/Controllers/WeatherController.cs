using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using WeatherApp.BL.Services;

namespace WeatherApp.API.Controllers;

[ApiController]
[Route("weather")]
public class WeatherController : ControllerBase
{
    private IConfiguration configuration;
    private WeatherService service;
    
    public WeatherController(IConfiguration configuration, IDistributedCache cache)
    {
        this.configuration = configuration;
        service = new WeatherService(this.configuration, cache);
    }
    [HttpGet]
    public async Task<IActionResult> GetPrecipitation([FromQuery] string city)
    {
        return Ok(await service.GetPrecipitation(city));
    }
}