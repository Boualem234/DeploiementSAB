using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using GameOn.Models;

namespace GameOn.ViewModels
{
    public partial class InfoJeuVM : ObservableObject
    {
        // Propriétés partagées à la vue
        [ObservableProperty]
        private Jeu? _jeu;

        public InfoJeuVM(Jeu jeu)
        {
            Jeu = jeu;
        }
    }
}
