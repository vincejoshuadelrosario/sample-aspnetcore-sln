using sample_dotnet_6_0.Models;

namespace sample_dotnet_6_0.Repositories
{
    public interface IWeatherForecastRepository
    {
        IEnumerable<WeatherForecast> Get();

        Task<IEnumerable<WeatherForecast>> GetAsync();
    }
}
