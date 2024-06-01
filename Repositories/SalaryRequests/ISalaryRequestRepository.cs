using PayrollManagementSystem.Models;

namespace PayrollManagementSystem.Repositories.SalaryRequests
{
    public interface ISalaryRequestRepository
    {
        Task<IEnumerable<SalaryRequest>> GetSalaryRequestsAsync();
        Task<SalaryRequest> GetSalaryRequestByIdAsync(int id);
        Task AddSalaryRequestAsync(SalaryRequest salaryRequest);
        Task UpdateSalaryRequestAsync(SalaryRequest salaryRequest);
        Task DeleteSalaryRequestAsync(int id);
        Task ApproveByAccountantAsync(int id);
        Task ApproveByManagerAsync(int id);
    }
}
