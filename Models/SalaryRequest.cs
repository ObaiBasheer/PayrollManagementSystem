using System.ComponentModel.DataAnnotations;

namespace PayrollManagementSystem.Models
{
    public class SalaryRequest
    {
        public int Id { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Amount { get; set; }
        public bool ApprovedByAccountant { get; set; }
        public bool ApprovedByManager { get; set; }
        public Employee? Employee { get; set; }
        [StringLength(255)]
        public string? AccountantDocumentPath { get; set; }
        [StringLength(255)]
        public string? ManagerDocumentPath { get; set; }
    }
}
