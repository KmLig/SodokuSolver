using SudokuSolverAPI.Models;

namespace SudokuSolverAPI.Repositories
{
    public interface ISudokuRepository
    {
        Task<IEnumerable<SudokuSolution>> GetAllSolutionsAsync();
        Task<SudokuSolution> GetSolutionByIdAsync(int id);
        Task AddSolutionAsync(SudokuSolution solution);
    }
}
