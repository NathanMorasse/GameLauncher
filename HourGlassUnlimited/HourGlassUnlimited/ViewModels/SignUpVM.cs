using HourGlassUnlimited.DataAccessLayer;
using HourGlassUnlimited.Models;
using HourGlassUnlimited.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HourGlassUnlimited.ViewModels
{
    public class SignUpVM : VM
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Confirmation { get; set; }
        public string Department { get; set; }

        public List<string> Departments { get; set; }

        public ICommand SignUp { get; set; }
        public ICommand GoBack { get; set; }

        public SignUpVM() : base()
        {
            SignUp = new CommandLink(SignUp_Execute, SignUp_CanExecute);
            GoBack = new CommandLink(GoBack_Execute, GoBack_CanExecute);

            Departments = new List<string>();
            DAL dal = new DAL();
            var temp = dal.Departments.All();

            foreach (Department item in temp)
            {
                Departments.Add(item.Name);
            }

            try
            {
                Department = Departments[0];
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Il n'y a aucun département dans la basse de donnée. Commencez par ajouter les départements pour pouvoir créer un utilisateur.");
            }
        }

        private bool SignUp_CanExecute(object parameter) { return true; }
        private void SignUp_Execute(object parameter) 
        {
            if (!(string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Confirmation)))
            {
                if (Password == Confirmation)
                {
                    string[] result = ConnectionHelper.SignUp(new User(0, Username, Password, Department));

                    Navigator.SignInView();
                }
            }
        }

        private bool GoBack_CanExecute(object parameter) { return true; }
        private void GoBack_Execute(object parameter)
        {
            Navigator.SignInView();
        }
    }
}
