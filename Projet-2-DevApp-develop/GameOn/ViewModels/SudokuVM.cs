using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameOn.Models;

namespace GameOn.ViewModels
{
    public partial class SudokuVM : ObservableObject
    {
        //Constantes
        const int NB_ESSAIS = 3;

        // Propriétés
        private Gestionnaire _gestionnaire;
        private Partie _partie;
        public event Action<string, bool> AfficherResultRequested;
        private DateTime _startTime;
        private TimeSpan _time;

        // Propriétés partagées à la vue
        [ObservableProperty]
        private string[] _plateauUtilisateur = new string[81];
        [ObservableProperty]
        private (int,int) _caseSelectionne = (-1,-1);
        [ObservableProperty]
        private int _essaie = 0;

        public SudokuVM(Gestionnaire gestionnaire)
        {
            _gestionnaire = gestionnaire;

            double differenceHeures;
            int? dureePartie = _gestionnaire.context.Jeux.FirstOrDefault(j => j.Nom == "Sudoku").Duree;
            foreach (Partie partie in _gestionnaire.context.Partie)
            {
                differenceHeures = (DateTime.Now - partie.Date.Value).TotalHours;
                if(differenceHeures < dureePartie)
                {
                    _partie = partie;
                }
            }

            for (int i = 0; i < 81; i++)
            {
                if(_partie.Plateau.ValuesPlateau[i] != '0')
                {
                    PlateauUtilisateur[i] = _partie.Plateau.ValuesPlateau[i].ToString();
                }
                else
                {
                    PlateauUtilisateur[i] = string.Empty;
                }
            }

            _startTime = DateTime.Now;
        }

        [RelayCommand]
        private void PlacerChiffre(string chiffre)
        {
            if (_partie.Plateau.ValuesPlateau[CaseSelectionne.Item2*9+CaseSelectionne.Item1] != '0') { return; }

            if (int.TryParse(chiffre, out int valeur))
            {
                var (x, y) = CaseSelectionne;

                if (x >= 0 && x < 9 && y >= 0 && y < 9)
                {
                    int index = y * 9 + x;

                    if (valeur == 0)
                    {
                        PlateauUtilisateur[index] = "";
                    }
                    else if (valeur >= 1 && valeur <= 9)
                    {
                        PlateauUtilisateur[index] = valeur.ToString();
                    }

                    OnPropertyChanged(nameof(PlateauUtilisateur));
                }
            }
        }

        [RelayCommand]
        private void VerifierPlateau()
        {
            string PlateauTempo = "";

            for(int i = 0; i < PlateauUtilisateur.Count(); i++)
            {
                PlateauTempo += PlateauUtilisateur[i];
            }

            if(_partie.Plateau.SolutionPlateau == PlateauTempo)
            {
                _time = DateTime.Now - _startTime;
                AfficherResultRequested?.Invoke($"Bravo, tu as réussi le Sudoku du jour en: {_time}", true);
            }
            else
            {
                Essaie++;
            }
        }

        partial void OnEssaieChanged(int value)
        {
            if(Essaie >= NB_ESSAIS)
            {
                _time = DateTime.Now - _startTime;
                AfficherResultRequested?.Invoke($"Malheureusement, tu as dépassé le nombre d'essais maximum ({NB_ESSAIS}/{NB_ESSAIS})    :(", false);
            }
        }
    }
}
