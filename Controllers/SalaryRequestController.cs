using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayrollManagementSystem.Models;
using PayrollManagementSystem.Models.ViewModel;
using PayrollManagementSystem.Services;

namespace PayrollManagementSystem.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class SalaryRequestController : ControllerBase
    {
        private readonly SalaryRequestService _salaryRequestService;

        public SalaryRequestController(SalaryRequestService salaryRequestService)
        {
            _salaryRequestService = salaryRequestService;
        }

        [HttpPost]
        [Authorize(Roles = "Accountant")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> CreateSalaryRequest([FromBody] SalaryRequestModel request)
        {
            var salaryRequest = new SalaryRequest()
            {
                Name = request.Name,
                IsApprovedByAccountant = request.IsApprovedByAccountant,
                IsApprovedByManager = request.IsApprovedByManager,
                AccountantDocumentPath = request.AccountantDocumentPath,
                ManagerDocumentPath = request.ManagerDocumentPath
            };
            var createdRequest = await _salaryRequestService.CreateSalaryRequestAsync(salaryRequest);
            return Ok(createdRequest);
        }

        [HttpPost("{requestId}/items")]
        [Authorize(Roles = "Accountant")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> AddSalaryRequestItem(int requestId, [FromBody] int salaryId)
        {
            var item = await _salaryRequestService.AddSalaryRequestItemAsync(requestId, salaryId);
            return Ok(item);
        }

        [HttpPost("{requestId}/approveByAccountant")]
        [Authorize(Roles = "Accountant")]
        public async Task<IActionResult> ApproveByAccountant(int requestId)
        {
            var request = await _salaryRequestService.ApproveSalaryRequestByAccountantAsync(requestId);
            return Ok(request);
        }

        [HttpPost("{requestId}/approveByManager")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> ApproveByManager(int requestId)
        {
            var request = await _salaryRequestService.ApproveSalaryRequestByManagerAsync(requestId);
            return Ok(request);
        }

        [HttpPost("{requestId}/reject")]
        [Authorize(Roles = "Accountant")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> RejectRequest(int requestId)
        {
            var request = await _salaryRequestService.RejectSalaryRequestAsync(requestId);
            return Ok(request);
        }
    }
}
