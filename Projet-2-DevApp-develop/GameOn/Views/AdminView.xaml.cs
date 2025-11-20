using GameOn.Models;
﻿using GameOn.ViewModels;
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
    /// Logique d'interaction pour AdminView.xaml
    /// </summary>
    public partial class AdminView : UserControl
    {
        Gestionnaire gestionnaire { get; set; }

        public AdminView(Gestionnaire Gestionnaire)
        {
            InitializeComponent();
            gestionnaire = Gestionnaire;
            AdminEmployeView.DataContext = new AdminEmployeVM(gestionnaire);
        }

        private void MenuEmploye_Click(object sender, RoutedEventArgs e)
        {
            AdminEmployeView.Visibility = Visibility.Visible;
        }
    }
}
