using System.Text.Json.Serialization;

namespace WeatherApp.DAL.Entities;


public class Day
{
    [JsonPropertyName("maxtemp_c")]
    public double MaxTemp { get; set; }
    
    [JsonPropertyName("mintemp_c")]
    public double MinTemp { get; set; }
    
    [JsonPropertyName("avgtemp_c")]
    public double AvgTemp { get; set; }
    
    [JsonPropertyName("maxwind_kph")]
    public double MaxWind { get; set; }
    
    [JsonPropertyName("totalprecip_mm")]
    public double TotalPercip { get; set; }
    
    [JsonPropertyName("totalsnow_cm")]
    public double TotalSnpw { get; set; }
    
    [JsonPropertyName("avghumidity")]
    public int AvgHumidity { get; set; }
    
    [JsonPropertyName("daily_will_it_rain")]
    public int DailyWillRain { get; set; }
    
    [JsonPropertyName("daily_chance_of_rain")]
    public int DailyChanceOfRain { get; set; }
    
    [JsonPropertyName("daily_will_it_snow")]
    public int DailyWillSnow { get; set; }
    
    [JsonPropertyName("daily_chance_of_snow")]
    public int DailyChanceOfSnow { get; set; }
    
    [JsonPropertyName("")]
    public Condition condition { get; set; }
}


