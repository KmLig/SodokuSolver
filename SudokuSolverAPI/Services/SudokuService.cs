using SudokuSolverAPI.Models;
using SudokuSolverAPI.Repositories;

namespace SudokuSolverAPI.Services
{
    public class SudokuService : ISudokuService
    {
        private readonly ISudokuRepository _repository;

        public SudokuService(ISudokuRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SudokuSolution>> GetAllSolutionsAsync()
        {
            return await _repository.GetAllSolutionsAsync();
        }

        public async Task<SudokuSolution> GetSolutionByIdAsync(int id)
        {
            return await _repository.GetSolutionByIdAsync(id);
        }

        public async Task SaveSolutionAsync(SudokuSolution solution)
        {
            await _repository.AddSolutionAsync(solution);
        }
    }
}
