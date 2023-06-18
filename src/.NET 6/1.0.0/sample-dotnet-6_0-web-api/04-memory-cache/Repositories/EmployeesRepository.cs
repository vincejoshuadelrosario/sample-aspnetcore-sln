using sample_dotnet_6_0.Data;
using sample_dotnet_6_0.Models;

namespace sample_dotnet_6_0.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly IDataAccess<Employee> _dataAccess;

        public EmployeesRepository(IDataAccess<Employee> dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<IEnumerable<Employee>> GetAsync() =>
            await _dataAccess.GetAsync();

        public async Task<Employee?> GetAsync(Guid id) =>
            await _dataAccess.GetAsync(id);

        public async Task<bool> AddAsync(Employee employee) =>
            await _dataAccess.AddAsync(employee);

        public async Task<bool> AddAsync(IEnumerable<Employee> employees) =>
            await _dataAccess.AddAsync(employees);

        public async Task<bool> UpdateAsync(Employee employee) =>
            await _dataAccess.UpdateAsync(employee);

        public async Task<bool> DeleteAsync(Guid id) =>
            await _dataAccess.DeleteAsync(id);
    }
}
