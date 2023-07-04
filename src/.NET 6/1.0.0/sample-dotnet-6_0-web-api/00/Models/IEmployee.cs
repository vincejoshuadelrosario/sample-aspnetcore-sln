namespace sample_dotnet_6_0.Models
{
    public interface IEmployee
    {
        string FirstName { get; }

        string LastName { get; }

        Guid Id { get; }
    }
}
