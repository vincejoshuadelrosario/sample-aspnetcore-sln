namespace sample_dotnet_6_0.Models
{
    public interface IEmployeeStats
    {
        IEmployee Employee { get; }

        int Reads { get; set; }

        DateTime Created { get; }
    }
}
