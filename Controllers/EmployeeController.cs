using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PayrollManagementSystem.Models;
using PayrollManagementSystem.Models.Validation;
using PayrollManagementSystem.Models.ViewModel;
using PayrollManagementSystem.Services;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Authorization;

namespace PayrollManagementSystem.Controllers
{
    [Route("api/Employees")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _service;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(EmployeeService service, ILogger<EmployeeController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedItems<EmployeeModel>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<Results<Ok<PaginatedItems<Employee>>, BadRequest<string>, StatusCodeHttpResult>> GetAllEmployees([FromQuery] PaginationRequest paginationRequest)
        {
            try
            {
                var pageSize = paginationRequest.PageSize;
                var pageIndex = paginationRequest.PageIndex;

                if (pageSize <= 0 || pageIndex < 0)
                {
                    return TypedResults.BadRequest("Invalid pagination parameters.");
                }

                var (employees, totalItems) = await _service.GetAllEmployeesAsync(pageSize, pageIndex);
                return TypedResults.Ok(new PaginatedItems<Employee>(pageIndex, pageSize, totalItems, employees));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching employees.");
                return TypedResults.StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApplicationUser), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<Results<Ok<Employee>, NotFound, BadRequest<string>, StatusCodeHttpResult>> GetEmployeeById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return TypedResults.BadRequest("Id is not valid.");
                }

                var employee = await _service.GetEmployeeByIdAsync(id);
                if (employee == null)
                {
                    return TypedResults.NotFound();
                }

                return TypedResults.Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the employee.");
                return TypedResults.StatusCode(500);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(Employee), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<Results<Created<Employee>, BadRequest<string>, StatusCodeHttpResult>> AddEmployee(EmployeeModel employee)
        {
            try
            {
                var validator = new EmployeeValidator();
                var validationResult = await validator.ValidateAsync(employee);

                if (!validationResult.IsValid)
                {
                    return TypedResults.BadRequest(string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
                }
                var emp = new Employee
                {
                    Name = employee.Name,
                    Salary = employee.Salary
                };

                await _service.AddEmployeeAsync(emp);
                return TypedResults.Created($"/api/Employees/{emp.Id}", emp);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the employee.");
                return TypedResults.StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
        {
            try
            {
                if (id != employee.Id)
                {
                    return BadRequest("Employee ID mismatch.");
                }

                var existingEmployee = await _service.GetEmployeeByIdAsync(id);
                if (existingEmployee == null)
                {
                    return NotFound("Employee not found.");
                }

                await _service.UpdateEmployeeAsync(employee);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the employee.");
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var existingEmployee = await _service.GetEmployeeByIdAsync(id);
                if (existingEmployee == null)
                {
                    return NotFound("Employee not found.");
                }

                await _service.DeleteEmployeeAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the employee.");
                return StatusCode(500, "An internal server error occurred.");
            }
        }
    }
}
