using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using WeatherApp.BL.Services;
namespace WeatherApp.API.Controllers;

[ApiController]
[Route("temperature")]
public class TemperatureController : ControllerBase
{
    private IConfiguration configuration;
    private IDistributedCache cache;
    private TemperatureService service;
    public TemperatureController(IConfiguration configuration, IDistributedCache cache)
    {
        this.configuration = configuration;
        this.service = new TemperatureService(configuration,cache);
    }
    
    [HttpGet("today")]
    public async Task<IActionResult> GetTodayTemperature([FromQuery] string city)
    {
        return Ok(await service.GetTodayWeather(city));
    }

    [HttpGet("todayByPoint")]
    public async Task<IActionResult> GetTodayTemperature([FromQuery] string longitude, [FromQuery] string latitude)
    {
        return Ok(await service.GetTodayWeather(longitude, latitude));
    }

    [HttpGet("tomorrow")]
    public async Task<IActionResult> GetTomorrowWeather([FromQuery] string city)
    {
        return Ok(await service.GetTomorrowWeather(city));
    }

    [HttpGet("yesterday")]
    public async Task<IActionResult> GetYesterdayWeather([FromQuery] string city)
    {
        return Ok(await service.GetYesterdayWeather(city));
    }

    [HttpGet("weekly")]
    public async Task<IActionResult> GetWeeklyWeather([FromQuery] string city)
    {
        var some = service.GetWeekWeather(city);
        return Ok(await service.GetWeekWeather(city));
    }
    
    [HttpGet("averageTemperature")]
    public async Task<IActionResult> GetWeeklyAverageTemperature([FromQuery] string city)
    {
        return Ok(await service.GetAverageWeeklyTemperature(city));
    }
}