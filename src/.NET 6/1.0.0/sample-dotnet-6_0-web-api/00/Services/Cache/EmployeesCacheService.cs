using sample_dotnet_6_0.Dto;
using sample_dotnet_6_0.Mappers;
using sample_dotnet_6_0.Models;
using sample_dotnet_6_0.Repositories.Cache;

namespace sample_dotnet_6_0.Services.Cache
{
    internal sealed class EmployeesCacheService : IEmployeesCacheService
    {
        private readonly IEmployeesCacheRepository _employeesCacheRepository;
        private readonly IEmployeeStatsRepository _employeeStatsRepository;

        public EmployeesCacheService(
            IEmployeesCacheRepository employeesCacheRepository,
            IEmployeeStatsRepository employeeStatsRepository)
        {
            _employeesCacheRepository = employeesCacheRepository;
            _employeeStatsRepository = employeeStatsRepository;
        }

        public async Task<IEnumerable<EmployeeReadDto>> GetAsync() =>
            (await _employeesCacheRepository.GetAsync()).Select(e => e.ToEmployeeReadDto());

        public async Task<EmployeeReadDto?> GetAsync(Guid id)
        {
            if (id == Guid.Empty) return null;

            var result = await _employeesCacheRepository.GetAsync(id);

            if (result is not null)
            {
                await _employeeStatsRepository.AddReadsAsync(result);
            }

            return result?.ToEmployeeReadDto();
        }

        public async Task<bool> AddAsync(EmployeeNewDto employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));

            return await _employeesCacheRepository.AddAsync(employee.ToEmployee());
        }

        public async Task<bool> AddAsync(IEnumerable<EmployeeNewDto> employees)
        {
            if (employees is null) throw new ArgumentNullException(nameof(employees));

            return await _employeesCacheRepository.AddAsync(employees.Select(e => e.ToEmployee()));
        }

        public async Task<bool> UpdateAsync(EmployeeUpdateDto employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));

            return await _employeesCacheRepository.UpdateAsync(employee.ToEmployee());
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));

            return await _employeesCacheRepository.DeleteAsync(id);
        }
    }
}
