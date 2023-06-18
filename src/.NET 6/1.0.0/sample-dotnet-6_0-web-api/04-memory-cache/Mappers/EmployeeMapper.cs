using sample_dotnet_6_0.Dto;
using sample_dotnet_6_0.Models;

namespace sample_dotnet_6_0.Mappers
{
    public static class EmployeeMapper
    {
        public static EmployeeReadDto ToEmployeeReadDto(this Employee employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));
            
            return new(employee.Id, employee.FirstName, employee.LastName);
        }

        public static Employee ToEmployee(this EmployeeNewDto employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));

            return new(employee.FirstName, employee.LastName);
        }

        public static Employee ToEmployee(this EmployeeUpdateDto employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));

            return new(employee.Id, employee.FirstName, employee.LastName);
        }
    }
}
