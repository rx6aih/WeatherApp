using WeatherApp.DAL.Entities;

namespace WeatherApp.BL.DTO;

public class FullDayWeatherDTO
{
    public Day Day { get; set; }
    public Hour[] Hour { get; set; }
}