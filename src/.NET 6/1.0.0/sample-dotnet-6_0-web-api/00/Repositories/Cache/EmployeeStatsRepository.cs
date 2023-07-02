using Microsoft.Extensions.Caching.Memory;
using Polly.Caching;
using Polly;
using Polly.Caching.Memory;
using sample_dotnet_6_0.Models;
using System.Collections.Concurrent;

namespace sample_dotnet_6_0.Repositories.Cache
{
    internal sealed class EmployeeStatsRepository : IEmployeeStatsRepository
    {
        // private static ConcurrentDictionary<IEmployee, IEmployeeStats>? EmployeeStatsCache;

        // private readonly IMemoryCache _memoryCache;
        private CachePolicy<ConcurrentDictionary<IEmployee, IEmployeeStats>> _cachePolicy;

        public EmployeeStatsRepository(
            // IMemoryCache memoryCache
            )
        {
            // _memoryCache = memoryCache;

            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var memoryCacheProvider = new MemoryCacheProvider(memoryCache);
            _cachePolicy = Policy.Cache<ConcurrentDictionary<IEmployee, IEmployeeStats>>(memoryCacheProvider, TimeSpan.FromSeconds(10));
        }

        public async Task<IEnumerable<IEmployeeStats>> GetAsync()
        {
            // await LoadCache();
            var employeeStatsCache = await GetCache();

            // return EmployeeStatsCache?.Values.AsEnumerable() ?? Enumerable.Empty<EmployeeStats>();
            return employeeStatsCache?.Values.AsEnumerable() ?? Enumerable.Empty<EmployeeStats>();
        }

        public async Task<IEmployeeStats> GetAsync(IEmployee employee)
        {
            // await LoadCache();
            var employeeStatsCache = await GetCache();

            IEmployeeStats? employeeStats = null;
            // EmployeeStatsCache?.TryGetValue(employee, out employeeStats);
            employeeStatsCache?.TryGetValue(employee, out employeeStats);

            return employeeStats ?? new EmployeeStats(employee, 0);
        }

        public async Task<bool> AddReadsAsync(IEmployee employee)
        {
            // await LoadCache();
            var employeeStatsCache = await GetCache();

            IEmployeeStats? employeeStats = null;
            // EmployeeStatsCache?.TryGetValue(employee, out employeeStats);
            employeeStatsCache?.TryGetValue(employee, out employeeStats);

            if (employeeStats is null)
            {
                // return EmployeeStatsCache?.TryAdd(employee, new EmployeeStats(employee, 1)) ?? false;
                return employeeStatsCache?.TryAdd(employee, new EmployeeStats(employee, 1)) ?? false;
            }

            // MUTATE?
            employeeStats.Reads += 1;

            return true;

            // var newEmployeeStats = (EmployeeStats)employeeStats with { Reads = employeeStats.Reads + 1 };

            // return employeeStatsCache?.TryUpdate(employee, newEmployeeStats, employeeStats) ?? false;
        }

        public async Task<bool> ResetAsync(IEmployee employee)
        {
            // await LoadCache();
            var employeeStatsCache = await GetCache();

            // return EmployeeStatsCache?.TryRemove(employee, out _) ?? false;
            return employeeStatsCache?.TryRemove(employee, out _) ?? false;
        }

        public Task ResetAsync()
        {
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var memoryCacheProvider = new MemoryCacheProvider(memoryCache);
            _cachePolicy = Policy.Cache<ConcurrentDictionary<IEmployee, IEmployeeStats>>(memoryCacheProvider, TimeSpan.FromSeconds(3600));

            return Task.CompletedTask;
        }

        //private async Task LoadCache()
        //{
        //    EmployeeStatsCache = _memoryCache.Get<ConcurrentDictionary<IEmployee, IEmployeeStats>>("employeesStats");

        //    if (EmployeeStatsCache is null)
        //    {
        //        await Task.Delay(3000);

        //        EmployeeStatsCache = new ConcurrentDictionary<IEmployee, IEmployeeStats>();

        //        _memoryCache.Set("employeesStats", EmployeeStatsCache, TimeSpan.FromSeconds(3600));
        //    }
        //}

        private Task<ConcurrentDictionary<IEmployee, IEmployeeStats>> GetCache() =>
            Task.FromResult(_cachePolicy.Execute(c => new ConcurrentDictionary<IEmployee, IEmployeeStats>(), new Context(nameof(EmployeeStatsRepository))));
    }
}
