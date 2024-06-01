using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayrollManagementSystem.Models;
using PayrollManagementSystem.Services;

namespace PayrollManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryListController : ControllerBase
    {
        private readonly SalaryListService _service;

        public SalaryListController(SalaryListService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSalaryLists()
        {
            var salaryLists = await _service.GetAllSalaryListsAsync();
            return Ok(salaryLists);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSalaryListById(int id)
        {
            var salaryList = await _service.GetSalaryListByIdAsync(id);
            if (salaryList == null)
            {
                return NotFound();
            }
            return Ok(salaryList);
        }

        [HttpPost]
        public async Task<IActionResult> AddSalaryList(SalaryList salaryList)
        {
            await _service.AddSalaryListAsync(salaryList);
            return CreatedAtAction(nameof(GetSalaryListById), new { id = salaryList.Id }, salaryList);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSalaryList(int id, SalaryList salaryList)
        {
            if (id != salaryList.Id)
            {
                return BadRequest();
            }

            await _service.UpdateSalaryListAsync(salaryList);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalaryList(int id)
        {
            await _service.DeleteSalaryListAsync(id);
            return NoContent();
        }
    }
}
