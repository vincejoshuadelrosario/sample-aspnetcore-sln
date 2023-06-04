using sample_dotnet_6_0.Models;

namespace sample_dotnet_6_0.Services
{
    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> Get();
        Task<IEnumerable<WeatherForecast>> GetAsync();
    }
}