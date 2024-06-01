using PayrollManagementSystem.Models;
using PayrollManagementSystem.Repositories.SalaryListRepo;

namespace PayrollManagementSystem.Services
{
    public class SalaryListService
    {
        private readonly ISalaryListRepository _repository;

        public SalaryListService(ISalaryListRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SalaryList>> GetAllSalaryListsAsync()
        {
            return await _repository.GetSalaryListsAsync();
        }

        public async Task<SalaryList> GetSalaryListByIdAsync(int id)
        {
            return await _repository.GetSalaryListByIdAsync(id);
        }

        public async Task AddSalaryListAsync(SalaryList salaryList)
        {
            await _repository.AddSalaryListAsync(salaryList);
        }

        public async Task UpdateSalaryListAsync(SalaryList salaryList)
        {
            await _repository.UpdateSalaryListAsync(salaryList);
        }

        public async Task DeleteSalaryListAsync(int id)
        {
            await _repository.DeleteSalaryListAsync(id);
        }
    }
}
