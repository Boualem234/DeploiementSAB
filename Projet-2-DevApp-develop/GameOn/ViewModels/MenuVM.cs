using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameOn.Models;
using GameOn.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GameOn.ViewModels
{
    public partial class MenuVM : ObservableObject
    {
        // Propriétés privées
        private Gestionnaire _gestionnaire;

        public MenuVM(Gestionnaire gestionnaire)
        {
            _gestionnaire = gestionnaire;
        }

        [RelayCommand]
        private void Logout()
        {
            _gestionnaire.EmployeConnecte = null;
            (Application.Current.MainWindow as MainWindow).RedirectionLogin();
        }
    }
}
