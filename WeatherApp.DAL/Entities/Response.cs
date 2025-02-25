namespace WeatherApp.DAL.Entities;

public class Response
{
    public Location location { get; set; }
    public Current current { get; set; }
    public Forecast forecast { get; set; }

}

public class Forecast
{
    public Forecastday[] forecastday { get; set; }
}





