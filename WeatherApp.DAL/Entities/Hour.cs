using System.Text.Json.Serialization;

namespace WeatherApp.DAL.Entities;


public class Hour
{
    [JsonPropertyName("time")]
    public string Time { get; set; }
    
    [JsonPropertyName("temp_c")]
    public double Tempirature { get; set; }
    
    [JsonPropertyName("is_day")]
    public int IsDay { get; set; }
    
    [JsonPropertyName("condition")]
    public Condition Condition { get; set; }
    
    [JsonPropertyName("wind_kph")]
    public double WindSpeed { get; set; }
    
    [JsonPropertyName("wind_degree")]
    public int WindDegree { get; set; }
    
    [JsonPropertyName("wind_dir")]
    public string WindDirection { get; set; }
    
    [JsonPropertyName("precip_mm")]
    public double Precipitation { get; set; }
    
    [JsonPropertyName("snow_cm")]
    public double SnowCm { get; set; }
    
    [JsonPropertyName("humidity")]
    public int Humidity { get; set; }
    
    [JsonPropertyName("cloud")]
    public int Cloud { get; set; }
    
    [JsonPropertyName("feelslike_c")]
    public double FeelsLike { get; set; }
    
    [JsonPropertyName("will_it_rain")]
    public int WillRain { get; set; }
    
    [JsonPropertyName("chance_of_rain")]
    public int RainChance { get; set; }
    
    [JsonPropertyName("will_it_snow")]
    public int WillSnow { get; set; }
    
    [JsonPropertyName("chance_of_snow")]
    public int ChanceToSnow { get; set; }
}