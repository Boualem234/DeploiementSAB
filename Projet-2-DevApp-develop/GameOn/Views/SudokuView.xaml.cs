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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameOn.Views
{
    /// <summary>
    /// Logique d'interaction pour SudokuView.xaml
    /// </summary>
    public partial class SudokuView : UserControl
    {
        public MenuView ParentMenu { get; set; }

        public SudokuView(Gestionnaire gestionnaire)
        {
            InitializeComponent();
            this.DataContext = new SudokuVM(gestionnaire);
            SudokuVM? vm = this.DataContext as SudokuVM;
            vm.AfficherResultRequested += AfficherResult;
        }

        public void CaseClick(object sender, RoutedEventArgs e)
        {
            SudokuVM? vm = this.DataContext as SudokuVM;
            (int, int) caseSelectionne = (-1, -1);

            if (sender is Button btn && btn.Tag is string tagStr)
            {
                string[] split = tagStr.Split(',');
                caseSelectionne = (int.Parse(split[0]), int.Parse(split[1]));
            }

            if (vm != null && caseSelectionne != (-1, -1))
            {
                vm.CaseSelectionne = caseSelectionne;
            }
        }

        private void Button_Quitter(object sender, RoutedEventArgs e)
        {
            ParentMenu?.AfficherListeJeu();
        }

        private void AfficherResult(string message, bool succes)
        {
            ParentMenu?.AfficherResult(message, succes);
        }
    }
}