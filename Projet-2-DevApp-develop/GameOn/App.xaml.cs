using System.Configuration;
using System.Data;
using System.Windows;
using AutoUpdaterDotNET;

namespace GameOn
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Lien RAW GitHub
            AutoUpdater.Start("https://raw.githubusercontent.com/Boualem234/DeploiementSAB/main/update/update.xml");

            base.OnStartup(e);
        }
    }

}
