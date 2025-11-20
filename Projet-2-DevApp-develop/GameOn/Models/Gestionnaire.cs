using GameOn.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOn.Models
{
    public class Gestionnaire
    {
        public readonly GameOnDbContext context = new GameOnDbContext();
        public Employe? EmployeConnecte { get; set; }
        public Departement? Departement { get; set; }
        public Entreprise? Entreprise { get; set; }
    }
}
