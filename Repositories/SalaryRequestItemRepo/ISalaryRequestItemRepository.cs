using PayrollManagementSystem.Models;

namespace PayrollManagementSystem.Repositories.SalaryRequestItemRepo
{
    public interface ISalaryRequestItemRepository
    {
        Task AddSalaryRequestItemAsync(SalaryRequestItem item);
    }
}
