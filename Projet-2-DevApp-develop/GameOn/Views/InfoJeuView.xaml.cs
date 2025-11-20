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
    /// Logique d'interaction pour InfoJeuView.xaml
    /// </summary>
    public partial class InfoJeuView : UserControl
    {
        public MenuView ParentMenu { get; set; }

        public InfoJeuView()
        {
            InitializeComponent();
        }

        private void Button_JouerJeu(object sender, RoutedEventArgs e)
        {
            ParentMenu?.AfficherSudoku();
        }

        private void Button_retour(object sender, RoutedEventArgs e)
        {
            ParentMenu?.AfficherListeJeu();
        }
    }
}