using sample_dotnet_6_0.Models;
using System.Collections.Concurrent;

namespace sample_dotnet_6_0.Data
{
    internal sealed class EmployeesDataAccess : IDataAccess<Employee>
    {
        private static readonly ConcurrentDictionary<Guid, Employee> Employees = new();

        public Task<IEnumerable<Employee>> GetAsync() =>
            Task.FromResult<IEnumerable<Employee>>(Employees.Values);

        public Task<Employee?> GetAsync(Guid id)
        {
            Employees.TryGetValue(id, out var employee);

            return Task.FromResult(employee);
        }

        public Task<bool> AddAsync(Employee newEmployee) =>
            Task.FromResult(Employees.TryAdd(newEmployee.Id, newEmployee));

        public Task<bool> AddAsync(IEnumerable<Employee> employees)
        {
            var result = false;

            foreach(var employee in employees)
            {
                result = Employees.TryAdd(employee.Id, employee);
                if (!result) break;
            }

            return Task.FromResult(result);
        }

        public Task<bool> UpdateAsync(Employee updatedEmployee)
        {
            Employees.TryGetValue(updatedEmployee.Id, out var oldEmployee);

            if (oldEmployee is null) return Task.FromResult(false);

            return Task.FromResult(Employees.TryUpdate(updatedEmployee.Id, updatedEmployee, oldEmployee));
        }

        public Task<bool> DeleteAsync(Guid id) =>
            Task.FromResult(Employees.TryRemove(id, out _));
    }
}
