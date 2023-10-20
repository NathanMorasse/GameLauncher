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
        public string Username { get; set; }
        public string Password { get; set; }

        public ICommand SignIn { get; set; }
        public ICommand GoToSignUp { get; set; }

        public SignInVM()
        {
            SignIn = new CommandLink(SignIn_Execute, SignIn_CanExecute);
            GoToSignUp = new CommandLink(GoToSignUp_Execute, GoToSignUp_CanExecute);
        }

        private bool SignIn_CanExecute(object parameter) { return true; }
        private void SignIn_Execute(object parameter) 
        { 
            string result = ConnectionHelper.SignIn(Username, Password);
            if (result == "Success")
            {
                Navigator.GameListView();
            }
        }

        private bool GoToSignUp_CanExecute(object parameter) { return true; }
        private void GoToSignUp_Execute(object parameter) { Navigator.SignUpView(); }
    }
}
