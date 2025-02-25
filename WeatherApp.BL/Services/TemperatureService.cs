using System.Net.Http.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using WeatherApp.BL.DTO;
using WeatherApp.BL.Utility;
using WeatherApp.DAL;
using WeatherApp.DAL.Entities;

namespace WeatherApp.BL.Services;

public class TemperatureService
{
    private readonly IConfiguration _configuration;
    public SideApi SideApi = new SideApi();
    private IDistributedCache _cache;
    public TemperatureService(IConfiguration configuration, IDistributedCache cache)
    {
        this._configuration = configuration;
        IConfiguration temp = _configuration.GetSection("SideApi");
        SideApi.ApiKey = temp.GetSection("ApiKey").Value;
        SideApi.BaseUrl = temp.GetSection("BaseUrl").Value;
        _cache = cache;
    }
    
    private HttpClient _httpClient = new HttpClient();
    public async Task<FullDayWeatherDTO> GetTodayWeather(string city)
    {
        string recordKey = "WeatherDAL_" +  city + DateTime.Now.ToString("yyyyMMdd");
        FullDayWeatherDTO forecast = await _cache.GetRecordAsync<FullDayWeatherDTO>(recordKey);

        if (forecast == null)
        {
            SideApi.ApiType = "forecast.json";
            SideApi.ApiParams = city;

            Response? response = await _httpClient.GetFromJsonAsync<Response>
                (SideApi.GetApiUrl());

            if (response == null)
                return null;

            FullDayWeatherDTO newForecast = new FullDayWeatherDTO()
            {
                Day = response.forecast.forecastday[0].day,
                Hour = response.forecast.forecastday[0].hour
            };

            await _cache.SetRecordAsync(recordKey, newForecast);

            return newForecast;
        }

        return forecast;
    }

    public async Task<FullDayWeatherDTO> GetTodayWeather(string latitude, string longitude)
    {
        string recordKey = "WeatherDAL_" +  latitude + longitude + DateTime.Now.ToString("yyyyMMdd");
        FullDayWeatherDTO forecast = await _cache.GetRecordAsync<FullDayWeatherDTO>(recordKey);

        if (forecast == null)
        {

            SideApi.ApiType = "forecast.json";
            SideApi.ApiParams = $"{latitude},{longitude}";
            Response? response = await _httpClient.GetFromJsonAsync<Response>
                (SideApi.GetApiUrl());

            if (response == null)
                return null;

            FullDayWeatherDTO newForecast = new FullDayWeatherDTO()
            {
                Day = response.forecast.forecastday[0].day,
                Hour = response.forecast.forecastday[0].hour
            };
            
            await _cache.SetRecordAsync(recordKey, newForecast);
            
            return newForecast;
        }
        
        return forecast;
    }

    public async Task<FullDayWeatherDTO> GetTomorrowWeather(string city)
    {
        string recordKey = "WeatherDAL_" +  city + DateTime.Now.ToString("yyyyMMdd") + " tomorrow";
        FullDayWeatherDTO forecast = await _cache.GetRecordAsync<FullDayWeatherDTO>(recordKey);

        if (forecast == null)
        {
            SideApi.ApiType = "forecast.json";
            SideApi.ApiParams = $"{city}";
            SideApi.OtherParams = "&days=2";
            string some = SideApi.GetApiUrl();
            Response? response = await _httpClient.GetFromJsonAsync<Response>(SideApi.GetApiUrl());
            if (response == null)
                return null;

            FullDayWeatherDTO newForecast = new FullDayWeatherDTO()
            {
                Day = response.forecast.forecastday[1].day,
                Hour = response.forecast.forecastday[1].hour
            };
            
            await _cache.SetRecordAsync(recordKey, newForecast);
            return newForecast;
        }
        return forecast;
    }
    
    public async Task<FullDayWeatherDTO> GetYesterdayWeather(string city)
    {
        string recordKey = "WeatherDAL_" +  city + DateTime.Now.ToString("yyyyMMdd") + " yestrday";
        FullDayWeatherDTO forecast = await _cache.GetRecordAsync<FullDayWeatherDTO>(recordKey);

        if (forecast == null)
        {
            SideApi.ApiType = "history.json";
            SideApi.ApiParams = city;
            SideApi.OtherParams = $"&dt={DateTime.Today.AddDays(-1):yyyy-M-d}";
            string some = SideApi.GetApiUrl();
            Response? response = await _httpClient.GetFromJsonAsync<Response>(SideApi.GetApiUrl());
            if (response == null)
                return null;

            FullDayWeatherDTO newForecast = new FullDayWeatherDTO()
            {
                Day = response.forecast.forecastday[0].day,
                Hour = response.forecast.forecastday[0].hour
            };
            
            await _cache.SetRecordAsync(recordKey, newForecast);
            return newForecast;
        }
        return forecast;
    }

    public async Task<WeeklyTemperatureDTO> GetWeekWeather(string city)
    {
        string recordKey = "WeatherDAL_" +  city + DateTime.Now.ToString("yyyyMMdd") + " week";
        WeeklyTemperatureDTO forecast = await _cache.GetRecordAsync<WeeklyTemperatureDTO>(recordKey);

        if (forecast == null)
        {
            SideApi.ApiType = "forecast.json";
            SideApi.ApiParams = city;
            SideApi.OtherParams = $"&days=7";

            Response? response = await _httpClient.GetFromJsonAsync<Response>(SideApi.GetApiUrl());

            WeeklyTemperatureDTO result = new WeeklyTemperatureDTO();

            foreach (var day in response.forecast.forecastday)
                result.Days.Add(new SimpleDayTemperatureDTO()
                {
                    Date = day.date,
                    AverageTemperature = day.day.AvgTemp.ToString(),
                    MinTemperature = day.day.MinTemp.ToString(),
                    MaxTemperature = day.day.MaxTemp.ToString(),
                });

            await _cache.SetRecordAsync(recordKey, result);
            return result;
        }
        return forecast;
    }

    public async Task<List<AverageWeeklyTemperatureDTO>> GetAverageWeeklyTemperature(string city)
    {
        string recordKey = "WeatherDAL_" +  city + DateTime.Now.ToString("yyyyMMdd") + " avgweek";
        List<AverageWeeklyTemperatureDTO> forecast = await _cache.GetRecordAsync<List<AverageWeeklyTemperatureDTO>>(recordKey);

        if (forecast == null)
        {
            SideApi.ApiType = "forecast.json";
            SideApi.ApiParams = city;
            SideApi.OtherParams = $"&days=7";

            List<AverageWeeklyTemperatureDTO> result = new List<AverageWeeklyTemperatureDTO>();

            Response? respone = await _httpClient.GetFromJsonAsync<Response>(SideApi.GetApiUrl());

            foreach (var day in respone.forecast.forecastday)
            {
                double temperature = day.hour[0].FeelsLike;

                foreach (var hour in day.hour)
                {
                    temperature += hour.FeelsLike;
                }

                result.Add(new AverageWeeklyTemperatureDTO()
                {
                    Date = day.date,
                    Temperature = (temperature / 24).ToString().Substring(0, 5),
                });
            }
            
            await _cache.SetRecordAsync(recordKey, result);
            return result;
        }
        return forecast;
    }
}