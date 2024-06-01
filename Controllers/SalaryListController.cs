using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayrollManagementSystem.Models;
using PayrollManagementSystem.Services;
using PayrollManagementSystem.Models.Validation;
using Microsoft.Extensions.Logging;

namespace PayrollManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryListController : ControllerBase
    {
        private readonly SalaryListService _service;
        private readonly ILogger<SalaryListController> _logger;

        public SalaryListController(SalaryListService service, ILogger<SalaryListController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SalaryList>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllSalaryLists()
        {
            try
            {
                var salaryLists = await _service.GetAllSalaryListsAsync();
                return Ok(salaryLists);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching salary lists.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SalaryList), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSalaryListById(int id)
        {
            try
            {
                var salaryList = await _service.GetSalaryListByIdAsync(id);
                if (salaryList == null)
                {
                    return NotFound("Salary list not found.");
                }
                return Ok(salaryList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the salary list.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(SalaryList), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddSalaryList(SalaryList salaryList)
        {
            try
            {
                var validator = new SalaryListValidator();
                var validationResult = await validator.ValidateAsync(salaryList);

                if (!validationResult.IsValid)
                {
                    return BadRequest(string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
                }

                await _service.AddSalaryListAsync(salaryList);
                return CreatedAtAction(nameof(GetSalaryListById), new { id = salaryList.Id }, salaryList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the salary list.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateSalaryList(int id, SalaryList salaryList)
        {
            try
            {
                if (id != salaryList.Id)
                {
                    return BadRequest("Salary list ID mismatch.");
                }

                var existingSalaryList = await _service.GetSalaryListByIdAsync(id);
                if (existingSalaryList == null)
                {
                    return NotFound("Salary list not found.");
                }

                await _service.UpdateSalaryListAsync(salaryList);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the salary list.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSalaryList(int id)
        {
            try
            {
                var existingSalaryList = await _service.GetSalaryListByIdAsync(id);
                if (existingSalaryList == null)
                {
                    return NotFound("Salary list not found.");
                }

                await _service.DeleteSalaryListAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the salary list.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }
    }
}
