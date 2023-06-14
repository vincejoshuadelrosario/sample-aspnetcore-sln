using Microsoft.Extensions.Caching.Memory;
using sample_dotnet_6_0.Data;
using sample_dotnet_6_0.Models;
using System.Collections.Concurrent;

namespace sample_dotnet_6_0.Repositories.Cache
{
    internal sealed class EmployeesCacheRepository : ICacheRepository<Employee>
    {
        private static ConcurrentDictionary<Guid, Employee>? EmployeesCache;

        private readonly IMemoryCache _memoryCache;
        private readonly IDataAccess<Employee> _dataAccess;

        public EmployeesCacheRepository(
            IMemoryCache memoryCache,
            IDataAccess<Employee> dataAccess)
        {
            _memoryCache = memoryCache;
            _dataAccess = dataAccess;
        }

        public async Task<IEnumerable<Employee>> GetAsync()
        {
            await LoadCache();

            return EmployeesCache?.Values.AsEnumerable() ?? Enumerable.Empty<Employee>();
        }

        public async Task<Employee?> GetAsync(Guid id)
        {
            await LoadCache();

            Employee? employee = null;
            EmployeesCache?.TryGetValue(id, out employee);

            return employee;
        }

        public async Task<bool> AddAsync(Employee newEmployee)
        {
            await LoadCache();

            return EmployeesCache?.TryAdd(newEmployee.Id, newEmployee) ?? false;
        }

        public async Task<bool> AddAsync(IEnumerable<Employee> employees)
        {
            await LoadCache();

            var result = false;

            foreach (var employee in employees)
            {
                result = EmployeesCache?.TryAdd(employee.Id, employee) ?? false;
                if (!result) break;
            }

            return result;
        }

        public async Task<bool> UpdateAsync(Employee updatedEmployee)
        {
            await LoadCache();

            Employee? oldEmployee = null;
            EmployeesCache?.TryGetValue(updatedEmployee.Id, out oldEmployee);

            if (oldEmployee is null) return false;

            return EmployeesCache?.TryUpdate(updatedEmployee.Id, updatedEmployee, oldEmployee) ?? false;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await LoadCache();
            
            return EmployeesCache?.TryRemove(id, out _) ?? false;
        }

        private async Task LoadCache()
        {
            EmployeesCache = _memoryCache.Get<ConcurrentDictionary<Guid, Employee>>("employees");

            if (EmployeesCache is null)
            {
                await Task.Delay(3000);

                EmployeesCache = new ConcurrentDictionary<Guid, Employee>();

                var employees = await _dataAccess.GetAsync();

                foreach(var employee in employees)
                {
                    EmployeesCache.TryAdd(employee.Id, employee);
                }

                _memoryCache.Set("employees", EmployeesCache, TimeSpan.FromSeconds(60));
            }
        }
    }
}
