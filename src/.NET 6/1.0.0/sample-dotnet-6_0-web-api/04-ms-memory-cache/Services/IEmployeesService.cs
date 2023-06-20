using sample_dotnet_6_0.Dto;
using sample_dotnet_6_0.Models;

namespace sample_dotnet_6_0.Services
{
    public interface IEmployeesService
    {
        Task<IEnumerable<EmployeeReadDto>> GetAsync();

        Task<EmployeeReadDto?> GetAsync(Guid id);

        Task<bool> AddAsync(EmployeeNewDto employee);

        Task<bool> AddAsync(IEnumerable<EmployeeNewDto> employee);

        Task<bool> UpdateAsync(EmployeeUpdateDto employee);

        Task<bool> DeleteAsync(Guid id);
    }
}
