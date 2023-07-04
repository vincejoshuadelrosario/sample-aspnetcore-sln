using sample_dotnet_6_0.Models;

namespace sample_dotnet_6_0.Repositories.Cache
{
    public interface IEmployeeStatsRepository
    {
        Task<IEnumerable<IEmployeeStats>> GetAsync();

        Task<IEmployeeStats> GetAsync(IEmployee employee);

        Task<bool> AddReadsAsync(IEmployee employee);

        Task<bool> ResetAsync(IEmployee employee);

        Task ResetAsync();
    }
}
