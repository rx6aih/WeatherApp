using System.Text.Json.Serialization;

namespace WeatherApp.DAL.Entities;

public class Current
{
    [JsonPropertyName("last_updated")]
    public string LastUpdated { get; set; }
    
    [JsonPropertyName("temp_c")]
    public double TemperatureC { get; set; }
    
    [JsonPropertyName("condition")]
    public Condition Condition { get; set; }

    [JsonPropertyName("wind_kph")]
    public double WindKph { get; set; }

    [JsonPropertyName("wind_dir")]
    public string WindDirection { get; set; }
    
    [JsonPropertyName("pressure_in")]
    public double Pressure { get; set; }
    
    [JsonPropertyName("cloud")]
    public int Cloud { get; set; }

    [JsonPropertyName("feelslike_c")]
    public double FeelsLikeC { get; set; }
}