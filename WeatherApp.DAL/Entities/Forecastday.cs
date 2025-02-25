namespace WeatherApp.DAL.Entities;

public class Forecastday
{
    public string date { get; set; }
    public Day day { get; set; }
    public Astro astro { get; set; }
    public Hour[] hour { get; set; }
}