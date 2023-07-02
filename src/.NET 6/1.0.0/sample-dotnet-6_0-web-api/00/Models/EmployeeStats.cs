using System.Xml;

namespace sample_dotnet_6_0.Models
{
    public sealed record EmployeeStats(IEmployee Employee, int Reads) : IEmployeeStats
    {
        public int Reads { get; set; } = Reads;

        public DateTime Created => DateTime.UtcNow;
    };
}
