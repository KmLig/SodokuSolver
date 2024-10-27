using SudokuSolverAPI.Data;
using SudokuSolverAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SudokuSolverAPI.Repositories
{
    public class SudokuRepository : ISudokuRepository
    {
        private readonly SudokuContext _context;

        public SudokuRepository(SudokuContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SudokuSolution>> GetAllSolutionsAsync()
        {
            return await _context.SudokuSolutions.ToListAsync();
        }

        public async Task<SudokuSolution> GetSolutionByIdAsync(int id)
        {
            var solution = await _context.SudokuSolutions.FindAsync(id);
            return solution ?? throw new KeyNotFoundException($"No solution found for id {id}");
        }

        public async Task AddSolutionAsync(SudokuSolution solution)
        {
            solution.SolvedAt = DateTime.UtcNow;
            await _context.SudokuSolutions.AddAsync(solution);
            await _context.SaveChangesAsync();
        }
    }
}
