using sample_dotnet_6_0.Models;

namespace sample_dotnet_6_0.Repositories.Cache
{
    public interface IEmployeesCacheRepository
    {
        Task<IEnumerable<Employee>> GetAsync();

        Task<Employee?> GetAsync(Guid id);

        Task<bool> AddAsync(Employee employee);

        Task<bool> AddAsync(IEnumerable<Employee> employees);

        Task<bool> UpdateAsync(Employee employee);

        Task<bool> DeleteAsync(Guid id);
    }
}
