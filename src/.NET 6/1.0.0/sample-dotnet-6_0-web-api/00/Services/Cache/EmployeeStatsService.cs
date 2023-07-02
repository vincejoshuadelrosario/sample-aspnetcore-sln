using sample_dotnet_6_0.Models;
using sample_dotnet_6_0.Repositories.Cache;

namespace sample_dotnet_6_0.Services.Cache
{
    internal sealed class EmployeeStatsService : IEmployeeStatsService
    {
        private readonly IEmployeeStatsRepository _employeeStatsRepository;

        public EmployeeStatsService(IEmployeeStatsRepository employeeStatsRepository)
        {
            _employeeStatsRepository = employeeStatsRepository;
        }

        public async Task<IEnumerable<IEmployeeStats>> GetAsync() =>
            await _employeeStatsRepository.GetAsync();

    }
}
