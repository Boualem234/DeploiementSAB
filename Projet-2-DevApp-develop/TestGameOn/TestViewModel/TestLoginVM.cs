using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameOn.Context;
using GameOn.Models;
using GameOn.ViewModels;

namespace TestGameOn.TestViewModel
{
    [TestClass]
    [DoNotParallelize]
    public class TestLoginVM
    {
        [TestMethod]
        public void CheckConnexion_IdMissing_SetsErrorIdRequired()
        {
            // Arrange
            Gestionnaire gestionnaire = new Gestionnaire();
            LoginVM vm = new LoginVM(gestionnaire);
            vm.Password = "dummy";
            vm.Id = null; // id manquant

            // Action
            vm.CheckConnexionCommand.Execute(null);

            // Assert
            Assert.AreEqual("ID requis", vm.Erreur);
        }

        [TestMethod]
        public void CheckConnexion_PasswordMissing_SetsErrorPasswordRequired()
        {
            // Arrange
            Gestionnaire gestionnaire = new Gestionnaire();
            LoginVM vm = new LoginVM(gestionnaire);
            vm.Id = 123;
            vm.Password = null; // mdp manquant

            // Action
            vm.CheckConnexionCommand.Execute(null);

            // Assert
            Assert.AreEqual("Mot de passe requis", vm.Erreur);
        }

        [TestMethod]
        public void CheckConnexion_InvalidCredentials_SetsErrorIdOrPasswordInvalid()
        {
            int testId = -111111;

            // Setup: insere un employe avec un mdp connu
            using (GameOnDbContext setupCtx = new GameOnDbContext())
            {
                Employe? existing = setupCtx.Employe.FirstOrDefault(e => e.Id == testId);
                if (existing != null)
                {
                    setupCtx.Employe.Remove(existing);
                    setupCtx.SaveChanges();
                }

                Employe emp = new Employe(testId, "invalid@test.com", "Nom", "Prenom", "rightpass", 2, true);
                setupCtx.Employe.Add(emp);
                setupCtx.SaveChanges();
            }

            // Arrange VM
            Gestionnaire gestionnaire = new Gestionnaire();
            LoginVM vm = new LoginVM(gestionnaire);
            vm.Id = testId;
            vm.Password = "wrongpass"; // mauvais mdp

            // Act
            vm.CheckConnexionCommand.Execute(null);

            //Asserts
            Assert.AreEqual("ID ou Mot de passe invalide", vm.Erreur);
            Assert.IsNull(vm.Id);
            Assert.AreEqual(string.Empty, vm.Password);

            using (GameOnDbContext cleanupCtx = new GameOnDbContext())
            {
                Employe? db = cleanupCtx.Employe.FirstOrDefault(e => e.Id == testId);
                if (db != null)
                {
                    cleanupCtx.Employe.Remove(db);
                    cleanupCtx.SaveChanges();
                }
            }
        }

    }
}