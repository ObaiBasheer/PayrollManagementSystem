namespace PayrollManagementSystem.Models
{
    public class SalaryList
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public decimal Salary { get; set; }
        public Employee? Employee { get; set; }
    }
}
