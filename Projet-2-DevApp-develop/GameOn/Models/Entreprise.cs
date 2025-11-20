using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOn.Models
{
    [Table("p2_entreprise")]
    public class Entreprise
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("nom")]
        public string? Nom { get; set; }

        public Entreprise() { }

        public Entreprise(int id, string? nom)
        {
            Id = id;
            Nom = nom;
        }
    }
}
