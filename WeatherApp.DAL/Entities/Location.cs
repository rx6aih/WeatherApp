using System.Text.Json.Serialization;

namespace WeatherApp.DAL.Entities;

public class Location
{
    [JsonPropertyName("name")] 
    public string Name { get; set; } = string.Empty;
    
    [JsonPropertyName("region")]
    public string Region { get; set; } = string.Empty;
    
    [JsonPropertyName("country")]
    public string Country { get; set; } = string.Empty;
    
    [JsonPropertyName("lat")]
    public double Latitude { get; set; }
    
    [JsonPropertyName("lon")]
    public double Longitude { get; set; }
    
    [JsonPropertyName("localtime")]
    public string LocalTime { get; set; }
}