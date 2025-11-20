using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOn.ViewModels
{
    public partial class ResultVM : ObservableObject
    {
        // Propriétés partagées à la vue
        [ObservableProperty]
        private string _titre = "Oups, Titre non défini.";
        [ObservableProperty]
        private string _message = "Oups, Message non défini.";

        public ResultVM(string message, bool succes)
        {
            Message = message;
            if (succes)
            {
                Titre = "Bravo, vous avez gagné!";
            }
            else
            {
                Titre = "Désolé, vous avez perdu!";
            }
        }
    }
}
