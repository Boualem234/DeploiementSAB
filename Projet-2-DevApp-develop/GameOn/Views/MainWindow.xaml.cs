using GameOn.Models;
using GameOn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GameOn.Views
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Gestionnaire gestionnaire = new Gestionnaire();
        
        MenuView menuV;
        public MainWindow()
        {
            InitializeComponent();

            loginV.DataContext = new LoginVM(gestionnaire);
            loginV.Visibility = Visibility.Visible;
                       
        }

        public void RedirectionMenu()
        {
            if (menuV == null)
            {
                menuV = new MenuView(gestionnaire);
                menuV.DataContext = new MenuVM(gestionnaire);
                MainGrid.Children.Add(menuV);
                Grid.SetRow(menuV, 1);
            }
            menuV.UpdateIsAdmin();

            loginV.Visibility = Visibility.Hidden;
            menuV.Visibility = Visibility.Visible;

            // Mettre à jour le header avec les infos de l'employé connecté
            if (gestionnaire.EmployeConnecte != null)
            {
                HeaderTextBlock.Text = $"{gestionnaire.EmployeConnecte.Prenom} {gestionnaire.EmployeConnecte.Nom} - {gestionnaire.Departement.Nom} - {gestionnaire.Entreprise.Nom}";
            }
        }

        public void RedirectionLogin()
        {
            loginV.Visibility = Visibility.Visible;
            if(menuV != null)
                menuV.Visibility = Visibility.Hidden;

            // Vider le header quand on retourne au login
            HeaderTextBlock.Text = "";
        }
    }

}