using SudokuSolverAPI.Models;

namespace SudokuSolverAPI.Services
{
    public interface ISudokuService
    {
        Task<IEnumerable<SudokuSolution>> GetAllSolutionsAsync();
        Task<SudokuSolution> GetSolutionByIdAsync(int id);
        Task SaveSolutionAsync(SudokuSolution solution);
    }
}
