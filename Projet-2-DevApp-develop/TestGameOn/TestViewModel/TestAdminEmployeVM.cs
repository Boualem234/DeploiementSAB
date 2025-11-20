using GameOn.Models;
using GameOn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGameOn.TestViewModel
{
    [TestClass]
    [DoNotParallelize]
    public partial class TestAdminEmployeVM
    {
        private Gestionnaire _gestionnaire;
        private AdminEmployeVM _vm;
        private Departement _dept;
        private Entreprise _ent;

        [TestInitialize]
        public void Init()
        {
            _gestionnaire = new Gestionnaire();

            _ent = _gestionnaire.context.Entreprise.FirstOrDefault();
            if (_ent == null)
            {
                _ent = new Entreprise { Id = 1, Nom = "GameOn Inc." };
                _gestionnaire.context.Entreprise.Add(_ent);
            }

            _dept = _gestionnaire.context.Departement.FirstOrDefault();
            if (_dept == null)
            {
                _dept = new Departement { Id = 1, Nom = "Informatique", Id_entreprise = _dept.Id };
                _gestionnaire.context.Departement.Add(_dept);
            }

            var admin = _gestionnaire.context.Employe.FirstOrDefault(e => e.Id == 1111111);
            if (admin == null)
            {
                admin = new Employe(1111111, "a@a.com", "a", "a", "Azerty123+", _dept.Id, true);
                _gestionnaire.context.Employe.Add(admin);
            }

            var emp = _gestionnaire.context.Employe.FirstOrDefault(e => e.Id == 67890);
            if (emp == null)
            {
                emp = new Employe(67890, "user@test.com", "Dupont", "Jean", "User", _dept.Id, true);
                _gestionnaire.context.Employe.Add(emp);
            }

            _gestionnaire.context.SaveChanges();

            // --- Configuration du gestionnaire ---
            _gestionnaire.EmployeConnecte = admin;
            _gestionnaire.Departement = _dept;
            _gestionnaire.Entreprise = _ent;

            // --- Création de la VM ---
            _vm = new AdminEmployeVM(_gestionnaire);
            _vm.EmployeSelectionne = emp;

        }

        [TestMethod]
        public void ModifierEmp_ModifierLesInformations()
        {
            _vm.Nom = "Tremblay";
            _vm.Courriel = "Tremblay.k@gmail.com";
            _vm.Prenom = "Katrine";
            _vm.Departement = _gestionnaire.context.Departement.First();
            _vm.PeutJouer = false;

            _vm.ModifierEmploye();

            var employeModifier = _gestionnaire.context.Employe.Find(_vm.EmployeSelectionne.Id);

            Assert.AreEqual("Tremblay", employeModifier.Nom);
            Assert.AreEqual("Katrine", employeModifier.Prenom);
            Assert.AreEqual("Tremblay.k@gmail.com", employeModifier.Courriel);
            Assert.IsFalse(employeModifier.PeutJouer);
        }

        [TestMethod]
        public void AjouterEmp_AjouterUnEmploye()
        {
            _vm.Nom = "Tremblay";
            _vm.Courriel = "Tremblay.k@gmail.com";
            _vm.Prenom = "Katrine";
            _vm.Departement = _gestionnaire.context.Departement.First();
            _vm.PeutJouer = false;

            _vm.SupprimerEmploye();
            _vm.AjouerEmploye();

            var empAjouter = _gestionnaire.context.Employe.FirstOrDefault(e => e.Courriel == "Tremblay.k@gmail.com");
            Assert.IsNotNull(empAjouter);
            Assert.AreEqual("Tremblay", empAjouter.Nom);
            Assert.AreEqual("Katrine", empAjouter.Prenom);
            Assert.AreEqual("Tremblay.k@gmail.com", empAjouter.Courriel);
            Assert.AreEqual(_dept.Id, empAjouter.IdDepartement);
            Assert.IsFalse(empAjouter.PeutJouer);

            Assert.IsNull(_vm.Erreur);

            _vm.SupprimerEmploye();
        }

        [TestMethod]
        public void SupprimerEmp_SupprimerUnEmploye()
        {
            var empasuppr = new Employe(99999, "b@b.com", "a", "a", "Password123+", _dept.Id, false);
            _gestionnaire.context.Employe.Add(empasuppr);
            _gestionnaire.context.SaveChanges();

            _vm.EmployeSelectionne = empasuppr;

            _vm.SupprimerEmploye();

            var empSupprimer = _gestionnaire.context.Employe.FirstOrDefault(e => e.Courriel == "b@b.com");
            Assert.IsNull(empSupprimer);

            Assert.IsNull(_vm.Erreur);
        }
    }
}
