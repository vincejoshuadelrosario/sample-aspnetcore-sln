namespace sample_dotnet_6_0.Models
{
    public sealed record Employee : IEmployee
    {
        public Employee(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public Employee(Guid id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; private init; }

        public string LastName { get; private init; }

        public Guid Id { get; private init; } = Guid.NewGuid();
    }
}
