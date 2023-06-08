using sample_dotnet_6_0.Models;
using sample_dotnet_6_0.Repositories;

namespace sample_dotnet_6_0.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {

        private readonly IWeatherForecastRepository _weatherForecastRepository;

        public WeatherForecastService(IWeatherForecastRepository weatherForecastRepository)
        {
            _weatherForecastRepository = weatherForecastRepository;
        }

        public async Task<IEnumerable<WeatherForecast>> GetAsync()
        {
            return await _weatherForecastRepository.GetAsync();
        }
    }
}
