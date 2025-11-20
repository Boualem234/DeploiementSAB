using GameOn.Models;
using GameOn.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameOn.Views
{
    /// <summary>
    /// Logique d'interaction pour MenuView.xaml
    /// </summary>
    public partial class MenuView : UserControl, INotifyPropertyChanged
    {
        Gestionnaire gestionnaire { get; set; }

        AdminView adminV;
        SudokuView sudokuV;
        ResultView resultV;

        private bool _isAdminUser;
        public bool IsAdminUser
        {
            get => _isAdminUser;
            set
            {
                if (_isAdminUser != value)
                {
                    _isAdminUser = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MenuView(Gestionnaire gestionnaire)
        {
            InitializeComponent();
            this.gestionnaire = gestionnaire;

            if (adminV == null)
            {
                adminV = new AdminView(gestionnaire);
                MainGrid.Children.Add(adminV);
                Grid.SetRow(adminV, 1);
            }

            jeuxV.ParentMenu = this;
            infoJeuV.ParentMenu = this;

            jeuxV.Visibility = Visibility.Visible;
            adminV.Visibility = Visibility.Hidden;
            infoJeuV.Visibility = Visibility.Hidden;
        }

        private void Button_Jouer(object sender, RoutedEventArgs e)
        {
            jeuxV.Visibility = Visibility.Visible;
            adminV.Visibility = Visibility.Hidden;
            infoJeuV.Visibility = Visibility.Hidden;
        }

        private void Button_Admin(object sender, RoutedEventArgs e)
        {
            jeuxV.Visibility = Visibility.Hidden;
            adminV.Visibility = Visibility.Visible;
            infoJeuV.Visibility = Visibility.Hidden;
        }

        public void AfficherInfoJeu()
        {
            jeuxV.Visibility = Visibility.Hidden;
            infoJeuV.Visibility = Visibility.Visible;
        }

        public void AfficherListeJeu()
        {
            jeuxV.Visibility = Visibility.Visible;
            infoJeuV.Visibility = Visibility.Hidden;
            sudokuV.Visibility = Visibility.Hidden;
            if(resultV != null)
                resultV.Visibility = Visibility.Hidden;
        }

        public void AfficherSudoku()
        {
            sudokuV = new SudokuView(gestionnaire);
            sudokuV.ParentMenu = this;
            MainGrid.Children.Add(sudokuV);
            Grid.SetRow(sudokuV, 1);

            sudokuV.Visibility = Visibility.Visible;
            infoJeuV.Visibility = Visibility.Hidden;
            jeuxV.Visibility = Visibility.Hidden;
        }

        public void AfficherResult(string message, bool succes)
        {
            sudokuV.Visibility = Visibility.Hidden;
            if (resultV == null)
            {
                resultV = new ResultView();
                resultV.ParentMenu = this;
                MainGrid.Children.Add(resultV);
                Grid.SetRow(resultV, 1);
            }
            resultV.DataContext = new ResultVM(message, succes);
            resultV.Visibility = Visibility.Visible;
        }

        public void UpdateIsAdmin()
        {
            IsAdminUser = gestionnaire?.Departement?.EstAdmin ?? false;

            // Met à jour directement la visibilité du bouton
            AdminButton.Visibility = IsAdminUser ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
