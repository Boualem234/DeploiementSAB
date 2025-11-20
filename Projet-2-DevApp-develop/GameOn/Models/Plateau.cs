using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOn.Models
{
    [Table("p2_sudoku_plateau")]
    public class Plateau
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("values_plateau")]
        public string? ValuesPlateau { get; set; }
        [Column("solution_plateau")]
        public string? SolutionPlateau { get; set; }

        public Plateau() { }

        public Plateau(int id, string? valuesPlateau, string? solutionPlateau )
        {
            Id = id;
            ValuesPlateau = valuesPlateau;
            SolutionPlateau = solutionPlateau;
        }

    }
}
