using System.ComponentModel.DataAnnotations;

namespace SudokuSolverAPI.Models
{
    public class SudokuSolution
    {
        [Key]  
        public int Id { get; set; }
        [Required(ErrorMessage = "Sudoku solution is required.")]
        [StringLength(81, ErrorMessage = "Sudoku solution must be exactly 81 characters.")]
        [RegularExpression("^[1-9]*$", ErrorMessage = "Invalid characters in Sudoku solution. Only numbers 1-9 are allowed.")]
        public string? Solution { get; set; }       
        public DateTime SolvedAt { get; set; }
    }
}
