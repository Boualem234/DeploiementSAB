using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameOn.Context;
using GameOn.Models;
using GameOn.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GameOn.ViewModels
{
    public partial class LoginVM : ObservableObject
    {
        // Propriétés privées
        private Gestionnaire _gestionnaire;

        // Propriétés partagées à la vue
        [ObservableProperty]
        private int? _id;
        [ObservableProperty]
        private string? _password;
        [ObservableProperty]
        private string? _erreur;

        public LoginVM(Gestionnaire gestionnaire)
        {
            _gestionnaire = gestionnaire;
        }

        [RelayCommand]
        private void CheckConnexion()
        {
            //Execptions
            if(Id == null) { Erreur = "ID requis"; return; }
            if(Password == null) { Erreur = "Mot de passe requis"; return; }

            //Récupère l'employe (null si inexistant)
            Employe? employe = Employe.Login(Id, Password);

            //Si l'employe n'existe pas
            if(employe == null)
            {
                Erreur = "ID ou Mot de passe invalide";
                Id = null;
                Password = "";
                return;
            }
            //Si l'employe existe, le set dans le gestionnaire et redirige vers le menu
            else
            {
                Erreur = "";
                _gestionnaire.EmployeConnecte = employe;
                _gestionnaire.Departement = _gestionnaire.context.Departement.FirstOrDefault(d => d.Id == employe.IdDepartement);
                if(_gestionnaire.Departement != null)
                    _gestionnaire.Entreprise = _gestionnaire.context.Entreprise.FirstOrDefault(e => e.Id == _gestionnaire.Departement.Id_entreprise);
                (Application.Current.MainWindow as MainWindow).RedirectionMenu();
            }
        }
    }
}
