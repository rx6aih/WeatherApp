namespace WeatherApp.BL.Utility;

public class SideApi
{
    public string BaseUrl { get; set; } = string.Empty;
    public string ApiType { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string ApiParams { get; set; } = string.Empty;
    
    public string OtherParams { get; set; } = string.Empty;

    public string GetApiUrl()
    {
        return this.BaseUrl + this.ApiType + "?" + this.ApiKey + "&q=" + this.ApiParams + this.OtherParams;
    }

}