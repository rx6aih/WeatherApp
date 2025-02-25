using WeatherApp.DAL.Entities;

namespace WeatherApp.BL.DTO;

public class WeeklyTemperatureDTO
{
    public List<SimpleDayTemperatureDTO> Days { get; set; } = new List<SimpleDayTemperatureDTO>(); 
}