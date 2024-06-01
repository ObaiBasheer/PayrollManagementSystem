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

        public async Task<IEnumerable<SalaryRequest>> GetSalaryRequestsAsync()
        {
            return await _context.SalaryRequests.Include(s => s.Employee).ToListAsync();
        }

        public async Task<SalaryRequest> GetSalaryRequestByIdAsync(int id)
        {
            return await _context.SalaryRequests.Include(s => s.Employee).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddSalaryRequestAsync(SalaryRequest salaryRequest)
        {
            _context.SalaryRequests.Add(salaryRequest);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSalaryRequestAsync(SalaryRequest salaryRequest)
        {
            _context.Entry(salaryRequest).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSalaryRequestAsync(int id)
        {
            var salaryRequest = await _context.SalaryRequests.FindAsync(id);
            if (salaryRequest != null)
            {
                _context.SalaryRequests.Remove(salaryRequest);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ApproveByAccountantAsync(int id)
        {
            var salaryRequest = await _context.SalaryRequests.FindAsync(id);
            if (salaryRequest != null)
            {
                salaryRequest.ApprovedByAccountant = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task ApproveByManagerAsync(int id)
        {
            var salaryRequest = await _context.SalaryRequests.FindAsync(id);
            if (salaryRequest != null)
            {
                salaryRequest.ApprovedByManager = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
