using PayrollManagementSystem.Models;

namespace PayrollManagementSystem.Repositories.Employees
{
    public interface IEmployeeRepository
    {
        Task<(IEnumerable<Employee> Employees, long Total)> GetEmployeesAsync(int pageSize, int pageIndex);
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task AddEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(int id);
    }
}
