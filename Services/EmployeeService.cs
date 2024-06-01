using PayrollManagementSystem.Models;
using PayrollManagementSystem.Repositories.Employees;

namespace PayrollManagementSystem.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<(IEnumerable<Employee> Employees, long Total)> GetAllEmployeesAsync(int pageSize, int pageIndex)
        {
            return await _repository.GetEmployeesAsync(pageSize, pageIndex);
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _repository.GetEmployeeByIdAsync(id);
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            await _repository.AddEmployeeAsync(employee);
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            await _repository.UpdateEmployeeAsync(employee);
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            await _repository.DeleteEmployeeAsync(id);
        }
    }
}
