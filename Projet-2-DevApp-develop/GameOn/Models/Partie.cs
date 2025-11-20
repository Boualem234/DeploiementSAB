using GameOn.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOn.Models
{
    [Table("p2_sudoku_partie")]
    public class Partie
    {
        private static readonly GameOnDbContext context = new GameOnDbContext();

        [Column("id")]
        public int Id { get; set; }
        [Column("id_plateau")]
        public int IdPlateau { get; set; }

        [Column("date")]
        public DateTime? Date { get; set; }

        [ForeignKey("IdPlateau")]
        public Plateau? Plateau { get { return context.Plateau.FirstOrDefault(p => p.Id == IdPlateau); } }

        public Partie() { }

        public Partie(int id, int idPlateau, DateTime? date)
        {
            Id = id;
            IdPlateau = idPlateau;
            Date = date;
            
        }
    }
}
