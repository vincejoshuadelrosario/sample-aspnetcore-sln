using sample_dotnet_6_0.Dto;
using sample_dotnet_6_0.Mappers;
using sample_dotnet_6_0.Repositories;

namespace sample_dotnet_6_0.Services
{
    internal sealed class EmployeesService : IEmployeesService
    {

        private readonly IEmployeesRepository _employeesRepository;

        public EmployeesService(IEmployeesRepository employeesRepository)
        {
            _employeesRepository = employeesRepository;
        }

        public async Task<IEnumerable<EmployeeReadDto>> GetAsync() =>
            (await _employeesRepository.GetAsync()).Select(e => e.ToEmployeeReadDto());

        public async Task<EmployeeReadDto?> GetAsync(Guid id)
        {
            if (id == Guid.Empty) return null;

            var result = await _employeesRepository.GetAsync(id);

            return result?.ToEmployeeReadDto();
        }

        public async Task<bool> AddAsync(EmployeeNewDto employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));

            return await _employeesRepository.AddAsync(employee.ToEmployee());
        }

        public async Task<bool> AddAsync(IEnumerable<EmployeeNewDto> employees)
        {
            if (employees is null) throw new ArgumentNullException(nameof(employees));

            return await _employeesRepository.AddAsync(employees.Select(e => e.ToEmployee()));
        }

        public async Task<bool> UpdateAsync(EmployeeUpdateDto employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));
            
            return await _employeesRepository.UpdateAsync(employee.ToEmployee());
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));

            return await _employeesRepository.DeleteAsync(id);
        }
    }
}
