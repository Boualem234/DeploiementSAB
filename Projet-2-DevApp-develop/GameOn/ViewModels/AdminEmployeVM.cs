using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameOn.Models;

namespace GameOn.ViewModels
{
    public partial class AdminEmployeVM : ObservableObject
    {
        // Propriétés privées
        private Gestionnaire _gestionnaire;

        // Propriétés partagées à la vue
        [ObservableProperty]
        private string? _nom;

        [ObservableProperty]
        private string? _prenom;

        [ObservableProperty]
        private string? _courriel;

        [ObservableProperty]
        private ObservableCollection<Departement> _departements;

        [ObservableProperty]
        private Departement _departement;

        [ObservableProperty]
        private bool _peutJouer = false;

        [ObservableProperty]
        private ObservableCollection<Employe> _employes;

        [ObservableProperty]
        private Employe _employeSelectionne;

        [ObservableProperty]
        private string? _erreur;

        public AdminEmployeVM(Gestionnaire gestionnaire)
        {
            _gestionnaire = gestionnaire;
            Departements = new (_gestionnaire.context.Departement.ToList().Where(d => d.Id_entreprise == _gestionnaire.Entreprise.Id));
            Employes = new(_gestionnaire.context.Employe.Where(e => _gestionnaire.context.Departement.Any(d => d.Id == e.IdDepartement && d.Id_entreprise == _gestionnaire.Entreprise.Id)).ToList());
            Employes.Remove(Employes.FirstOrDefault(e => e.Id == _gestionnaire.EmployeConnecte.Id));
        }
        partial void OnEmployeSelectionneChanged(Employe value)
        {
            if (value != null)
            {
                Nom = value.Nom;
                Prenom = value.Prenom;
                Courriel = value.Courriel;
                Departement = value.Departement;
                PeutJouer = value.PeutJouer;
            }
            else
            {
                Nom = null;
                Prenom = null;
                Courriel = null;
                Departement = null;
                PeutJouer = false;
            }
            AjouerEmployeCommand.NotifyCanExecuteChanged();
            ModifierEmployeCommand.NotifyCanExecuteChanged();
            SupprimerEmployeCommand.NotifyCanExecuteChanged();
        }

        [RelayCommand(CanExecute = nameof(PeutAjouterEmploye))]
        public void AjouerEmploye()
        {
            //Exceptions
            Erreur = null;
            if (Nom == null || Nom == "") { Erreur = "Nom requis"; return; }
            if (Prenom == null || Prenom == "") { Erreur = "Prenom requis"; return; }
            if (Courriel == null || Courriel == "") { Erreur = "Courriel requis"; return; }
            if (Departement == null) { Erreur = "Departement requis"; return; }
            if (_gestionnaire.context.Employe.FirstOrDefault(e => e.Courriel == Courriel) != null) { Erreur = "Courriel déjà existant"; return; }

            //Génération de l'id
            int id;
            do
            {
                //Trouve les 5 premier chiffres aléatoirement
                id = Random.Shared.Next(10000, 100000);
                //Ttrouve les 2 derniers chiffre avec l'années (2025 à 25)
                id += (DateTime.Now.Year % 100) * 100000;
            } while (_gestionnaire.context.Employe.FirstOrDefault(e => e.Id == id) != null);

            //Ajoute l'employe
            Employe newEmploye = new Employe(id, Courriel, Nom, Prenom, null, Departement.Id, PeutJouer);
            Employes.Add(newEmploye);
            _gestionnaire.context.Employe.Add(newEmploye);
            _gestionnaire.context.SaveChanges();

            //Reset les valeurs du formulaire
            Nom = null;
            Prenom = null;
            Courriel = null;
            Departement = null;
            PeutJouer = false;
        }

        [RelayCommand(CanExecute = nameof(PeutModifierEmploye))]
        public void ModifierEmploye()
        {
            //Exceptions
            Erreur = null;
            if (Nom == null || Nom == "") { Erreur = "Nom requis"; return; }
            if (Prenom == null || Prenom == "") { Erreur = "Prenom requis"; return; }
            if (Courriel == null || Courriel == "") { Erreur = "Courriel requis"; return; }
            if (Departement == null) { Erreur = "Departement requis"; return; }
            if (Courriel != EmployeSelectionne.Courriel)
                if (_gestionnaire.context.Employe.FirstOrDefault(e => e.Courriel == Courriel) != null) { Erreur = "Courriel déjà existant"; return; }

            //Garde l'id de l'employe
            int id = EmployeSelectionne.Id;

            //Met à jour les données de l'employe
            EmployeSelectionne.Nom = Nom;
            EmployeSelectionne.Prenom = Prenom;
            EmployeSelectionne.Courriel = Courriel;
            EmployeSelectionne.IdDepartement = Departement.Id;
            EmployeSelectionne.Departement = Departement;
            EmployeSelectionne.PeutJouer = PeutJouer;

            //Sauvegarde l'employe dans la listview et dans la bd 
            _gestionnaire.context.SaveChanges();
            Employes.Remove(EmployeSelectionne);
            Employes.Add(_gestionnaire.context.Employe.Find(id));

        }

        [RelayCommand(CanExecute = nameof(PeutModifierEmploye))]
        public void SupprimerEmploye()
        {
            Erreur = null;
            if (EmployeSelectionne != null)
            {
                _gestionnaire.context.Remove(EmployeSelectionne);
                Employes.Remove(EmployeSelectionne);
                _gestionnaire.context.SaveChanges();
            }
        }

        public bool PeutAjouterEmploye()
        {
            if (EmployeSelectionne == null)
            {
                return true;
            }
            return false;
        }
        public bool PeutModifierEmploye()
        {
            if (EmployeSelectionne == null)
            {
                return false;
            }
            return true;
        }
    }
}
