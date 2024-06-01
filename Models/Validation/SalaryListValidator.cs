using FluentValidation;

namespace PayrollManagementSystem.Models.Validation
{
    public class SalaryListValidator : AbstractValidator<SalaryList>
    {
        public SalaryListValidator()
        {
            RuleFor(s => s.EmployeeId).GreaterThan(0).WithMessage("EmployeeId must be greater than 0.");

            RuleFor(s => s.Salary).GreaterThan(0).WithMessage("Salary must be greater than 0.");

            
        }
    }

}
