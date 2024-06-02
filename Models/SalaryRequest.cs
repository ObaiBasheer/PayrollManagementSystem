using System.ComponentModel.DataAnnotations;

namespace PayrollManagementSystem.Models
{
    public class SalaryRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsApprovedByAccountant { get; set; }
        public bool IsApprovedByManager { get; set; }
        public List<SalaryRequestItem> Items { get; set; }
        public string? AccountantDocumentPath { get; set; }
        public string? ManagerDocumentPath { get; set; }
    }
}

