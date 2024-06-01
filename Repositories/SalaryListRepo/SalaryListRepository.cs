using Microsoft.EntityFrameworkCore;
using PayrollManagementSystem.Data;
using PayrollManagementSystem.Models;

namespace PayrollManagementSystem.Repositories.SalaryListRepo
{
    public class SalaryListRepository : ISalaryListRepository
    {
        private readonly ApplicationDbContext _context;

        public SalaryListRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SalaryList>> GetSalaryListsAsync()
        {
            return await _context.SalaryLists.Include(s => s.Employee).ToListAsync();
        }

        public async Task<SalaryList> GetSalaryListByIdAsync(int id)
        {
            return await _context.SalaryLists.Include(s => s.Employee).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddSalaryListAsync(SalaryList salaryList)
        {
            _context.SalaryLists.Add(salaryList);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSalaryListAsync(SalaryList salaryList)
        {
            _context.Entry(salaryList).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSalaryListAsync(int id)
        {
            var salaryList = await _context.SalaryLists.FindAsync(id);
            if (salaryList != null)
            {
                _context.SalaryLists.Remove(salaryList);
                await _context.SaveChangesAsync();
            }
        }
    }
}
