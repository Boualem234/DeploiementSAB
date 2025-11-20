using GameOn.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOn.Context
{
    public class GameOnDbContext : DbContext
    {
        public DbSet<Employe> Employe { get; set; }
        public DbSet<Departement> Departement { get; set; }
        public DbSet<Entreprise> Entreprise { get; set; }
        public DbSet<Jeu> Jeux { get; set; }
        public DbSet<Plateau> Plateau { get; set; }
        public DbSet<Partie> Partie { get; set; }

        /// <summary>
        /// Set la chaine de connection SQL
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=sql.decinfo-cchic.ca;Port=33306;Database=a25_equipe2_dev_app_expert;Uid=dev-2534056;Pwd=cruipi72failou";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

    }
}
