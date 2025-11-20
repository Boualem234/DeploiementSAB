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
    /// Logique d'interaction pour InfoJeuView.xaml
    /// </summary>
    public partial class JeuxView : UserControl
    {
        public MenuView ParentMenu { get; set; }
        public JeuxView()
        {
            Gestionnaire gestionnaire = new Gestionnaire();
            InitializeComponent();
            DataContext = new JeuxVM(gestionnaire);
        }

        private void Button_Jouer(object sender, RoutedEventArgs e)
        {
            ParentMenu?.AfficherInfoJeu();
        }
    }
}