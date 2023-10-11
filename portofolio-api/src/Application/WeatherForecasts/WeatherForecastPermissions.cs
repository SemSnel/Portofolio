using SemSnel.Portofolio.Domain.Identity;

namespace SemSnel.Portofolio.Application.WeatherForecasts;

public static class WeatherForecastPermissions
{
    public static readonly Permission WeatherForecastsPermission = "CAN_ACCESS_WEATHER_FORECASTS_MODULE";
    public static readonly Permission GetWeatherForecastPermission = "CAN_GET_WEATHER_FORECAST";
    public static readonly Permission CreateWeatherForecastPermission = "CAN_CREATE_WEATHER_FORECAST";
    public static readonly Permission UpdateWeatherForecastPermission = "CAN_UPDATE_WEATHER_FORECAST";
    public static readonly Permission DeleteWeatherForecastPermission = "CAN_DELETE_WEATHER_FORECAST";
}