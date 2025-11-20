using GameOn.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOn.Models
{
    [Table("p2_jeu")]
    public class Jeu
    {
        private static readonly GameOnDbContext context = new GameOnDbContext();

        [Column("id")]
        public int Id { get; set; }
        [Column("nom")]
        public string? Nom { get; set; }

        [Column("regles")]
        public string? Regles { get; set; }

        [Column("nb_joueurs")]
        public int? NbJoueurs { get; set; }

        [Column("duree")]
        public int? Duree { get; set; }

        public Jeu() { }

        public Jeu(int id, string? nom, string? regles, int? nbJoueurs, int? duree)
        {
            Id = id;
            Nom = nom;
            Regles = regles;
            NbJoueurs = nbJoueurs;
            Duree = duree;
        }

        public static async Task<List<Jeu>> GetListJeuxAsync()
        {
            try
            {
                List<Jeu> jeux = await context.Jeux.ToListAsync();

                if (jeux == null || jeux.Count == 0)
                {
                    Console.WriteLine("Aucun jeu trouvé dans la base de données.");
                    return new List<Jeu>();
                }

                return jeux;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération des jeux : {ex.Message}");
                return new List<Jeu>();
            }
        }

    }
}
