using GameOn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using static MaterialDesignThemes.Wpf.Theme;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GameOn.Views
{
    /// <summary>
    /// Logique d'interaction pour LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
            this.IsVisibleChanged += UserControl_IsVisibleChanged;
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.IsVisible)
            {
                IDTextBox.Text = string.Empty;
                PasswordBox.Password = null;
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var pwdBox = sender as System.Windows.Controls.PasswordBox;
            if (pwdBox == null) return;

            string rawPassword = "";

            rawPassword = pwdBox.Password;

            LoginVM? vm = this.DataContext as LoginVM;
            if (vm == null) return;

            // Valider le mot de passe brut
            string? validationError = ValidatePassword(rawPassword);

            if (!string.IsNullOrEmpty(validationError))
            {
                vm.Erreur = validationError;
                vm.Password = null;
            }
            else
            {
                vm.Erreur = "";
                vm.Password = ComputeSha256Hash(rawPassword);
            }
        }


        /// <summary>
        /// Hash un string en SHA256 (recup sur internet)
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        private static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var vm = DataContext as LoginVM;
                if (vm != null && vm.CheckConnexionCommand.CanExecute(null))
                {
                    vm.CheckConnexionCommand.Execute(null);
                }
            }
        }

        private string? ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return "Mot de passe requis";

            if (password.Length < 8)
                return "Le mot de passe doit contenir au moins 8 caractères";

            if (!password.Any(char.IsUpper))
                return "Le mot de passe doit contenir au moins une majuscule";

            if (!password.Any(char.IsLower))
                return "Le mot de passe doit contenir au moins une minuscule";

            if (!password.Any(char.IsDigit))
                return "Le mot de passe doit contenir au moins un chiffre";

            string specialChars = "!@#$%^&*()_-+=<>?";
            if (!password.Any(c => specialChars.Contains(c)))
                return "Le mot de passe doit contenir au moins un caractère spécial";

            return null; // valide
        }

    }
}
