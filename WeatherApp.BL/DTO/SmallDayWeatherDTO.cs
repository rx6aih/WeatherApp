namespace WeatherApp.BL.DTO;

public class SmallDayWeatherDTO
{
    public string Date { get; set; } = string.Empty;
    public string Precipitation { get; set; } = string.Empty;
    public string WindKmPerHour { get; set; } = string.Empty;
    
    public string Sunrise { get; set; } = string.Empty;
    public string Sunset { get; set; } = string.Empty;
}