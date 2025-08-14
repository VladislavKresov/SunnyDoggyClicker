using Newtonsoft.Json;

public class WeatherForecast {
    public string Temperature;
    public string TemperatureUnit;

    public static WeatherForecast FromJson(string json) {
        dynamic jsonData = JsonConvert.DeserializeObject(json);
        var firstPeriod = jsonData.properties.periods[0];

        return new WeatherForecast {
            Temperature = firstPeriod.temperature.ToString(),
            TemperatureUnit = firstPeriod.temperatureUnit.ToString()
        };
    }
}
