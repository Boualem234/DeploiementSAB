using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOn.Models
{
    [Table("p2_departement")]
    public class Departement
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("nom")]
        public string? Nom { get; set; }
        [Column("id_entreprise")]
        public int Id_entreprise { get; set; }
        [Column("peut_jouer")]
        public bool PeutJouer { get; set; }
        [Column("est_admin")]
        public bool EstAdmin { get; set; }

        public Departement() { }

        public Departement(int id, string? nom, int id_entreprise, bool peutJouer, bool estAdmin)
        {
            Id = id;
            Nom = nom;
            Id_entreprise = id_entreprise;
            PeutJouer = peutJouer;
            EstAdmin = estAdmin;
        }
    }
}
