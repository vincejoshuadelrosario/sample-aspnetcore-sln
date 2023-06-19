using Microsoft.Extensions.Caching.Memory;
using Polly;
using Polly.Caching;
using Polly.Caching.Memory;
using sample_dotnet_6_0.Data;
using sample_dotnet_6_0.Models;
using System.Collections.Concurrent;

namespace sample_dotnet_6_0.Repositories.Cache
{
    internal sealed class EmployeesCacheRepository : ICacheRepository<Employee>
    {
        private readonly CachePolicy<ConcurrentDictionary<Guid, Employee>> _cachePolicy;
        private readonly IDataAccess<Employee> _dataAccess;

        public EmployeesCacheRepository(
            IMemoryCache memoryCache,
            IDataAccess<Employee> dataAccess)
        {
            var memoryCacheProvider = new MemoryCacheProvider(memoryCache);
            _cachePolicy = Policy.Cache<ConcurrentDictionary<Guid, Employee>>(memoryCacheProvider, TimeSpan.FromSeconds(60));
            _dataAccess = dataAccess;
        }

        public Task<IEnumerable<Employee>> GetAsync() =>
            Task.FromResult(LoadCache().Values.AsEnumerable() ?? Enumerable.Empty<Employee>());

        public Task<Employee?> GetAsync(Guid id)
        {
            Employee? employee = null;
            LoadCache().TryGetValue(id, out employee);

            return Task.FromResult(employee);
        }

        public Task<bool> AddAsync(Employee newEmployee) =>
            Task.FromResult(LoadCache().TryAdd(newEmployee.Id, newEmployee));

        public Task<bool> AddAsync(IEnumerable<Employee> employees)
        {
            var result = false;

            foreach (var employee in employees)
            {
                result = LoadCache().TryAdd(employee.Id, employee);
                if (!result) break;
            }

            return Task.FromResult(result);
        }

        public Task<bool> UpdateAsync(Employee updatedEmployee)
        {
            Employee? oldEmployee = null;
            LoadCache().TryGetValue(updatedEmployee.Id, out oldEmployee);

            return Task.FromResult(oldEmployee is null
                ? false
                : LoadCache().TryUpdate(updatedEmployee.Id, updatedEmployee, oldEmployee));

        }

        public Task<bool> DeleteAsync(Guid id) =>
            Task.FromResult(LoadCache().TryRemove(id, out _));

        private ConcurrentDictionary<Guid, Employee> LoadCache() =>
            _cachePolicy.Execute(_ => InitializeCacheAsync().Result, new Context(nameof(Employee)));

        private async Task<ConcurrentDictionary<Guid, Employee>> InitializeCacheAsync()
        {
            await Task.Delay(3000);

            var cache = new ConcurrentDictionary<Guid, Employee>();

            var employees = await _dataAccess.GetAsync();

            foreach (var employee in employees)
            {
                cache.TryAdd(employee.Id, employee);
            }

            return cache;
        }
    }
}
