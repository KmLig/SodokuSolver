using Microsoft.EntityFrameworkCore;
using SudokuSolverAPI.Models;

namespace SudokuSolverAPI.Data
{
    public class SudokuContext : DbContext
    {
        public SudokuContext(DbContextOptions<SudokuContext> options) : base(options) { }

        public DbSet<SudokuSolution> SudokuSolutions { get; set; }
    }
}
