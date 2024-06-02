using Microsoft.EntityFrameworkCore;
using PayrollManagementSystem.Data;
using PayrollManagementSystem.Models;

namespace PayrollManagementSystem.Repositories.SalaryRequests
{
    public class SalaryRepository : ISalaryRequestRepository
    {
        private readonly ApplicationDbContext _context;

        public SalaryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SalaryRequest> GetSalaryRequestByIdAsync(int requestId)
        {
            return await _context.SalaryRequests.Include(r => r.Items).ThenInclude(i => i.Salary).FirstOrDefaultAsync(r => r.Id == requestId);
        }

        public async Task AddSalaryRequestAsync(SalaryRequest request)
        {
            _context.SalaryRequests.Add(request);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSalaryRequestAsync(SalaryRequest request)
        {
            _context.SalaryRequests.Update(request);
            await _context.SaveChangesAsync();
        }
    }
}
