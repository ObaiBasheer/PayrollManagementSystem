namespace PayrollManagementSystem.Models
{
    public class SalaryRequestItem
    {
        public int Id { get; set; }
        public int SalaryRequestId { get; set; }
        public int SalaryId { get; set; }
        public SalaryList Salary { get; set; }
    }
}
