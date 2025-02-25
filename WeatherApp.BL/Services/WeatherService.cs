using System.Net.Http.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using WeatherApp.BL.DTO;
using WeatherApp.BL.Utility;
using WeatherApp.DAL;
using WeatherApp.DAL.Entities;

namespace WeatherApp.BL.Services;

public class WeatherService
{
    private readonly IConfiguration configuration;
    private SideApi SideApi = new SideApi();
    private HttpClient _httpClient = new HttpClient();
    private IDistributedCache _cache;

    public WeatherService(IConfiguration configuration, IDistributedCache cache)
    {
        this.configuration = configuration;
        IConfiguration temp = configuration.GetSection("SideApi");
        SideApi.ApiKey = temp.GetSection("ApiKey").Value;
        SideApi.BaseUrl = temp.GetSection("BaseUrl").Value;
        _cache = cache;
    }

    public async Task<List<SmallDayWeatherDTO>> GetPrecipitation(string city)
    {
        string recordKey = "WeatherDAL_" + city + DateTime.Now.ToString("yyyyMMdd") + " percip";
        List<SmallDayWeatherDTO> forecast = await _cache.GetRecordAsync<List<SmallDayWeatherDTO>>(recordKey);

        if (forecast == null)
        {
            SideApi.ApiType = "forecast.json";
            SideApi.ApiParams = city;
            SideApi.OtherParams = "&days=7";

            Response? response = await _httpClient.GetFromJsonAsync<Response>(SideApi.GetApiUrl());

            if (response == null)
                return null;

            List<SmallDayWeatherDTO> result = new List<SmallDayWeatherDTO>();

            foreach (var day in response.forecast.forecastday)
            {
                bool willPrecipitation = false;
                double windSpeed = 0;
                foreach (var hour in day.hour)
                {
                    if (hour.WillRain == 1 || hour.WillSnow == 2)
                        willPrecipitation = true;

                    windSpeed += hour.WindSpeed;
                }

                result.Add(new SmallDayWeatherDTO()
                {
                    Date = day.date,
                    Precipitation = willPrecipitation.ToString(),
                    WindKmPerHour = (windSpeed / 24).ToString().Substring(0, 1),
                    Sunrise = day.astro.sunrise,
                    Sunset = day.astro.sunset
                });
            }
            
            await _cache.SetRecordAsync(recordKey, result);
            
            return result;
        }
        
        return forecast;
    }
}