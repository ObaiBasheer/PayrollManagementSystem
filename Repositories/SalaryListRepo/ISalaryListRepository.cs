using PayrollManagementSystem.Models;

namespace PayrollManagementSystem.Repositories.SalaryListRepo
{
    public interface ISalaryListRepository
    {
        Task<IEnumerable<SalaryList>> GetSalaryListsAsync();
        Task<SalaryList> GetSalaryListByIdAsync(int id);
        Task AddSalaryListAsync(SalaryList salaryList);
        Task UpdateSalaryListAsync(SalaryList salaryList);
        Task DeleteSalaryListAsync(int id);
    }
}
