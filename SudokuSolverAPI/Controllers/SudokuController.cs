using Microsoft.AspNetCore.Mvc;
using SudokuSolverAPI.Models;
using SudokuSolverAPI.Services;

namespace SudokuSolverAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SudokuController : ControllerBase
    {
        private readonly ISudokuService _service;

        public SudokuController(ISudokuService service)
        {
            _service = service;
        }

        // GET: api/sudoku
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SudokuSolution>>> GetAllSolutions()
        {
            var solutions = await _service.GetAllSolutionsAsync();
            return Ok(solutions);
        }

        // GET: api/sudoku/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SudokuSolution>> GetSolutionById(int id)
        {
            var solution = await _service.GetSolutionByIdAsync(id);
            if (solution == null)
            {
                return NotFound();
            }
            return Ok(solution);
        }

        // POST: api/sudoku/solve
        [HttpPost("solve")]
        public async Task<ActionResult> SolveSudoku([FromBody] SudokuSolution solution)
        {
            if (solution == null || string.IsNullOrEmpty(solution.Solution))
            {
                return BadRequest("Invalid Sudoku solution.");
            }

            await _service.SaveSolutionAsync(solution);
            return Ok(new { message = "Solution saved successfully" });
        }
    }
}
