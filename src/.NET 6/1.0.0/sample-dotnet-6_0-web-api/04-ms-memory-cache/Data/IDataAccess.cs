namespace sample_dotnet_6_0.Data
{
    public interface IDataAccess<T>
    {
        Task<IEnumerable<T>> GetAsync();

        Task<T?> GetAsync(Guid id);

        Task<bool> AddAsync(T val);

        Task<bool> AddAsync(IEnumerable<T> val);

        Task<bool> UpdateAsync(T val);

        Task<bool> DeleteAsync(Guid id);
    }
}
