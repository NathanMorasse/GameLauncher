using HourGlassUnlimited.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HourGlassUnlimited.ViewModels
{
    public class MainWindowVM : VM
    {
        public ICommand GoToGameList { get; set; }
        public ICommand GoToRankings { get; set; }
        public ICommand GoToEditAccount { get; set; }
        public ICommand GoToSignIn { get; set; }

        public MainWindowVM() 
        {
            this.GoToGameList = new CommandLink(GoToGameList_Execute, GoToGameList_CanExecute);
            this.GoToRankings = new CommandLink(GoToRankings_Execute, GoToRankings_CanExecute);
            this.GoToEditAccount = new CommandLink(GoToEditAccount_Execute, GoToEditAccount_CanExecute);
            this.GoToSignIn = new CommandLink(GoToSignIn_Execute, GoToSignIn_CanExecute);
        }

        private bool GoToGameList_CanExecute(object parameter) { return true; }
        private void GoToGameList_Execute(object parameter) { Navigator.GameListView(); }

        private bool GoToRankings_CanExecute(object parameter) { return true; }
        private void GoToRankings_Execute(object parameter) { Navigator.RankingsView(); }

        private bool GoToEditAccount_CanExecute(object parameter) { return true; }
        private void GoToEditAccount_Execute(object parameter) { Navigator.EditAccountView(); }

        private bool GoToSignIn_CanExecute(object parameter) { return true; }
        private void GoToSignIn_Execute(object parameter) { Navigator.SignInView(); }
    }
}
