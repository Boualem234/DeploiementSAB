using GameOn.Context;
using GameOn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGameOn.TestModel
{
    [TestClass]
    [DoNotParallelize]
    public class TestEmploye
    {
        // NOTE: On n'utilise pas de DbContext partagé pour éviter les opérations concurrentes.
        // Chaque test crée/detruit un DbContext local pour le setup.

        [TestMethod]
        public void Login_NewEmployee_SetsPasswordAndReturnsEmploye()
        {
            int testId = -999999;

            // utiliser un DbContext local pour préparer la donnée
            using (GameOnDbContext setupCtx = new GameOnDbContext())
            {
                Employe? existing = setupCtx.Employe.FirstOrDefault(e => e.Id == testId);
                if (existing != null)
                {
                    setupCtx.Employe.Remove(existing);
                    setupCtx.SaveChanges();
                }

                Employe? emp = new Employe(testId, "test@example.com", "Morel", "Olivier", null, 2, true);
                setupCtx.Employe.Add(emp);
                setupCtx.SaveChanges();
            }

            // appeler la fonction pour se logger
            Employe? result = Employe.Login(testId, "secret123");

            Assert.IsNotNull(result, "Le login doit retourner l'employé lorsque le mot de passe était null et qu'il est défini.");

            // verifier avec un nouveau context distinct
            using (GameOnDbContext verifyCtx = new GameOnDbContext())
            {
                Employe? db = verifyCtx.Employe.FirstOrDefault(e => e.Id == testId);
                Assert.IsNotNull(db);
                Assert.AreEqual("secret123", db.MotDePasse, "Le mot de passe doit être enregistré en base après le premier login.");

                // supprimer la ligne de test créée
                verifyCtx.Employe.Remove(db);
                verifyCtx.SaveChanges();
            }
        }

        [TestMethod]
        public void Login_WrongPassword_ReturnsNull()
        {
            int testId = -999998;

            // créer l'employé via un contexte local (supprime d'abord l'éventuelle ligne existante)
            using (GameOnDbContext setupCtx = new GameOnDbContext())
            {
                Employe? existing = setupCtx.Employe.FirstOrDefault(e => e.Id == testId);
                if (existing != null)
                {
                    setupCtx.Employe.Remove(existing);
                    setupCtx.SaveChanges();
                }

                Employe? emp = new Employe(testId, "test2@example.com", "Nom", "Prenom", null, 2, true);
                setupCtx.Employe.Add(emp);
                setupCtx.SaveChanges();
            }

            // Premier login : définit le mot de passe
            Employe? first = Employe.Login(testId, "rightpass");
            Assert.IsNotNull(first, "Le premier login doit réussir et définir le mot de passe.");

            // Tentative avec mauvais mot de passe -> doit retourner null
            Employe? second = Employe.Login(testId, "wrongpass");
            Assert.IsNull(second, "Un mot de passe incorrect doit empêcher la connexion.");

            // Nettoyage via un nouveau contexte
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

        [TestMethod]
        public void Login_NullId_ReturnsNull()
        {
            // Vérifie que la tentative de login avec un id null retourne null
            Employe? result = Employe.Login(null, "whatever");
            Assert.IsNull(result, "Login avec un identifiant null doit retourner null.");
        }
    }
}
