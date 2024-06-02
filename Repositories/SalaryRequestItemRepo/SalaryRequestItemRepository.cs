using PayrollManagementSystem.Data;
using PayrollManagementSystem.Models;
using PayrollManagementSystem.Repositories.SalaryRequestItemRepo;

namespace PayrollManagementSystem.Repositories.SalaryRequestItemRepo
{
    public class SalaryRequestItemRepository : ISalaryRequestItemRepository
    {
        private readonly ApplicationDbContext _context;

        public SalaryRequestItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddSalaryRequestItemAsync(SalaryRequestItem item)
        {
            _context.SalaryRequestItems.Add(item);
            await _context.SaveChangesAsync();
        }
    }
}
