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
        private ICommand _SignIn;
        public ICommand SignIn 
        { 
            get { return _SignIn; } 
            set { _SignIn = value; } 
        }

        public SignInVM()
        {
            this.SignIn = new CommandLink(SignIn_Execute, SignIn_CanExecute);
        }

        private bool SignIn_CanExecute(object parameter) { return true; }
        private void SignIn_Execute(object parameter)
        {
            Navigator.GameListView();
        }
    }
}
