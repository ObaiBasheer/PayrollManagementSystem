using Microsoft.AspNetCore.Mvc;
using PayrollManagementSystem.Models;
using PayrollManagementSystem.Repositories.SalaryListRepo;
using PayrollManagementSystem.Repositories.SalaryRequestItemRepo;
using PayrollManagementSystem.Repositories.SalaryRequests;

namespace PayrollManagementSystem.Services
{
    public class SalaryRequestService
    {
        private readonly ISalaryListRepository _salaryRepository;
        private readonly ISalaryRequestRepository _salaryRequestRepository;
        private readonly ISalaryRequestItemRepository _salaryRequestItemRepository;

        public SalaryRequestService(
            ISalaryListRepository salaryRepository,
            ISalaryRequestRepository salaryRequestRepository,
            ISalaryRequestItemRepository salaryRequestItemRepository)
        {
            _salaryRepository = salaryRepository;
            _salaryRequestRepository = salaryRequestRepository;
            _salaryRequestItemRepository = salaryRequestItemRepository;
        }

        public async Task<SalaryRequest> CreateSalaryRequestAsync(SalaryRequest request)
        {
            await _salaryRequestRepository.AddSalaryRequestAsync(request);
            return request;
        }

        public async Task<SalaryRequestItem> AddSalaryRequestItemAsync(int requestId, int salaryId)
        {
            var salary = await _salaryRepository.GetSalaryListByIdAsync(salaryId);
            if (salary == null) throw new Exception("Salary not found");

            var request = await _salaryRequestRepository.GetSalaryRequestByIdAsync(requestId);
            if (request == null) throw new Exception("Request not found");

            var item = new SalaryRequestItem
            {
                SalaryRequestId = requestId,
                SalaryId = salaryId,
                Salary = salary
            };

            await _salaryRequestItemRepository.AddSalaryRequestItemAsync(item);
            return item;
        }

        public async Task<SalaryRequest> ApproveSalaryRequestByAccountantAsync(int requestId)
        {
            var request = await _salaryRequestRepository.GetSalaryRequestByIdAsync(requestId);
            if (request == null) throw new Exception("Request not found");

            request.IsApprovedByAccountant = true;
            await _salaryRequestRepository.UpdateSalaryRequestAsync(request);
            return request;
        }

        public async Task<SalaryRequest> ApproveSalaryRequestByManagerAsync(int requestId)
        {
            var request = await _salaryRequestRepository.GetSalaryRequestByIdAsync(requestId);
            if (request == null) throw new Exception("Request not found");

            if (!request.IsApprovedByAccountant) throw new Exception("Request not approved by accountant");

            request.IsApprovedByManager = true;
            await _salaryRequestRepository.UpdateSalaryRequestAsync(request);
            return request;
        }

        public async Task<SalaryRequest> RejectSalaryRequestAsync(int requestId)
        {
            var request = await _salaryRequestRepository.GetSalaryRequestByIdAsync(requestId);
            if (request == null) throw new Exception("Request not found");

            request.IsApprovedByAccountant = false;
            request.IsApprovedByManager = false;
            await _salaryRequestRepository.UpdateSalaryRequestAsync(request);
            return request;
        }

    }




}
