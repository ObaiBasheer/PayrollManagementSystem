using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayrollManagementSystem.Models;
using PayrollManagementSystem.Services;

namespace PayrollManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
 
    [ApiController]
    public class SalaryRequestController : ControllerBase
    {
        private readonly SalaryRequestService _service;
        public SalaryRequestController(SalaryRequestService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSalaryRequests()
        {
            var salaryRequests = await _service.GetAllSalaryRequestsAsync();
            return Ok(salaryRequests);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSalaryRequestById(int id)
        {
            var salaryRequest = await _service.GetSalaryRequestByIdAsync(id);
            if (salaryRequest == null)
            {
                return NotFound();
            }
            return Ok(salaryRequest);
        }

        [HttpPost]
        public async Task<IActionResult> AddSalaryRequest(SalaryRequest salaryRequest)
        {
            await _service.AddSalaryRequestAsync(salaryRequest);
            return CreatedAtAction(nameof(GetSalaryRequestById), new { id = salaryRequest.Id }, salaryRequest);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSalaryRequest(int id, SalaryRequest salaryRequest)
        {
            if (id != salaryRequest.Id)
            {
                return BadRequest();
            }

            await _service.UpdateSalaryRequestAsync(salaryRequest);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalaryRequest(int id)
        {
            await _service.DeleteSalaryRequestAsync(id);
            return NoContent();
        }

        //[Authorize(Roles = "Accountant")]
        [HttpPost("{id}/approve-accountant")]
        public async Task<IActionResult> ApproveByAccountant(int id,  IFormFile document)
        {
            var documentPath = Path.Combine("Uploads", document.FileName);
            using (var stream = new FileStream(documentPath, FileMode.Create))
            {
                await document.CopyToAsync(stream);
            }
            await _service.ApproveRequestByAccountantAsync(id, documentPath);
            return NoContent();
        }

        //[Authorize(Roles = "Manager")]
        [HttpPost("{id}/approve-manager")]
        public async Task<IActionResult> ApproveByManager(int id, IFormFile document)
        {
            var documentPath = Path.Combine("Uploads", document.FileName);
            using (var stream = new FileStream(documentPath, FileMode.Create))
            {
                await document.CopyToAsync(stream);
            }
            await _service.ApproveRequestByManagerAsync(id, documentPath);
            return NoContent();
        }
    }
}
