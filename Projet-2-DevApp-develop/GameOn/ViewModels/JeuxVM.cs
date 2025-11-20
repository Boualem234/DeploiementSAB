using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameOn.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOn.ViewModels
{
    public partial class JeuxVM : ObservableObject
    {
        private readonly Gestionnaire _gestionnaire;

        [ObservableProperty]
        private ObservableCollection<Jeu> jeux = new();

        public JeuxVM(Gestionnaire gestionnaire)
        {
            _gestionnaire = gestionnaire;
            LoadJeuxAsync();
        }

        private async void LoadJeuxAsync()
        {
            var listeJeux = await Jeu.GetListJeuxAsync();
            Jeux.Clear();
            foreach (var jeu in listeJeux)
                Jeux.Add(jeu);
        }

        [RelayCommand]
        private void PlayGame()
        {

        }
    }
}
