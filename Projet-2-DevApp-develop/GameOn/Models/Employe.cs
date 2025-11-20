using GameOn.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOn.Models
{
    [Table("p2_employe")]
    public class Employe
    {
        private static readonly GameOnDbContext context = new GameOnDbContext();

        [Column("id")]
        public int Id { get; set; }
        [Column("courriel")]
        public string? Courriel { get; set; }
        [Column("nom")]
        public string? Nom { get; set; }
        [Column("prenom")]
        public string? Prenom { get; set; }
        [Column("mot_de_passe")]
        public string? MotDePasse { get; set; }
        [Column("id_departement")]
        public int IdDepartement { get; set; }
        [Column("peut_jouer")]
        public bool PeutJouer { get; set; }
        [ForeignKey("IdDepartement")]
        public Departement? Departement { get; set; }

        public Employe() { }

        public Employe(int id, string? courriel, string? nom, string? prenom, string? mot_de_passe, int id_departement, bool peut_jouer)
        {
            Id = id;
            Courriel = courriel;
            Nom = nom;
            Prenom = prenom;
            MotDePasse = mot_de_passe;
            IdDepartement = id_departement;
            PeutJouer = peut_jouer;
        }

        /// <summary>
        /// Retourne l'employe, sinon retourne null
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static Employe? Login(int? id, string password)
        {
            //Get l'employe selon l'id
            Employe? employe = context.Employe.FirstOrDefault(e => e.Id == id);

            //Execption
            if (employe == null) { return null; }

            //Set le mdp de l'employe si il n'en a pas encore
            if(employe.MotDePasse == null) 
            {
                employe.MotDePasse = password;
                context.SaveChanges();
                return employe;
            }

            //Si les infos sont correctes -> return true
            if(employe.Id == id && employe.MotDePasse == password)
            {
                return employe;
            }

            //Si les infos sont incorrectes -> return false
            return null;
        }
    }
}
