using Microsoft.AspNetCore.Mvc;
using PayrollManagementSystem.Models;

namespace PayrollManagementSystem.Repositories.SalaryRequests
{
    public interface ISalaryRequestRepository
    {
        Task<SalaryRequest> GetSalaryRequestByIdAsync(int requestId);
        Task AddSalaryRequestAsync(SalaryRequest request);
        Task UpdateSalaryRequestAsync(SalaryRequest request);
    }
}
