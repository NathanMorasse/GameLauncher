using HourGlassUnlimited.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HourGlassUnlimited.ViewModels
{
    public class SignInVM : VM
    {
        public ICommand GoToGameList { get; set; }
        public ICommand GoToSignUp { get; set; }

        public SignInVM()
        {
            GoToGameList = new CommandLink(GoToGameList_Execute, GoToGameList_CanExecute);
            GoToSignUp = new CommandLink(GoToSignUp_Execute, GoToSignUp_CanExecute);
        }

        private bool GoToGameList_CanExecute(object parameter) { return true; }
        private void GoToGameList_Execute(object parameter) { Navigator.GameListView(); }

        private bool GoToSignUp_CanExecute(object parameter) { return true; }
        private void GoToSignUp_Execute(object parameter) { Navigator.SignUpView(); }
    }
}
