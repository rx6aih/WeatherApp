namespace WeatherApp.BL.DTO;

public class SimpleDayTemperatureDTO
{
    public string Date { get; set; } = string.Empty;
    public string AverageTemperature { get; set; } = string.Empty;
    public string MinTemperature { get; set; } = string.Empty;
    public string MaxTemperature { get; set; } = string.Empty;
}