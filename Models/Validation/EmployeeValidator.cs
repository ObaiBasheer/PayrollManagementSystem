using FluentValidation;
using PayrollManagementSystem.Models.ViewModel;

namespace PayrollManagementSystem.Models.Validation
{
    public class EmployeeValidator : AbstractValidator<EmployeeModel>
    {
        public EmployeeValidator()
        {
            RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
            RuleFor(e => e.Salary).GreaterThanOrEqualTo(0);
        }
    }
}
