using Microsoft.AspNetCore.Mvc;
using PayrollManagementSystem.Models;
using PayrollManagementSystem.Repositories.SalaryRequests;

namespace PayrollManagementSystem.Services
{
    public class SalaryRequestService
    {
        private readonly ISalaryRequestRepository _repository;

        public SalaryRequestService(ISalaryRequestRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<SalaryRequest>> GetAllSalaryRequestsAsync()
        {
            return await _repository.GetSalaryRequestsAsync();
        }

        public async Task<SalaryRequest> GetSalaryRequestByIdAsync(int id)
        {
            return await _repository.GetSalaryRequestByIdAsync(id);
        }

        public async Task AddSalaryRequestAsync(SalaryRequest salaryRequest)
        {
            await _repository.AddSalaryRequestAsync(salaryRequest);
        }

        public async Task UpdateSalaryRequestAsync(SalaryRequest salaryRequest)
        {
            await _repository.UpdateSalaryRequestAsync(salaryRequest);
        }

        public async Task DeleteSalaryRequestAsync(int id)
        {
            await _repository.DeleteSalaryRequestAsync(id);
        }
        public async Task ApproveRequestByAccountantAsync(int id, string documentPath)
        {
            var salaryRequest = await _repository.GetSalaryRequestByIdAsync(id);
            if (salaryRequest != null)
            {
                salaryRequest.ApprovedByAccountant = true;
                salaryRequest.AccountantDocumentPath = documentPath;
                await _repository.UpdateSalaryRequestAsync(salaryRequest);
            }
        }

        public async Task ApproveRequestByManagerAsync(int id, string documentPath)
        {
            var salaryRequest = await _repository.GetSalaryRequestByIdAsync(id);
            if (salaryRequest != null)
            {
                salaryRequest.ApprovedByManager = true;
                salaryRequest.ManagerDocumentPath = documentPath;
                await _repository.UpdateSalaryRequestAsync(salaryRequest);
            }
        }


    }




}
