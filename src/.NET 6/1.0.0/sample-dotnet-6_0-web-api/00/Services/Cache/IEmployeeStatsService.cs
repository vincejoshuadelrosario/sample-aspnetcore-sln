using sample_dotnet_6_0.Models;

namespace sample_dotnet_6_0.Services.Cache
{
    public interface IEmployeeStatsService
    {
        Task<IEnumerable<IEmployeeStats>> GetAsync();
    }
}
