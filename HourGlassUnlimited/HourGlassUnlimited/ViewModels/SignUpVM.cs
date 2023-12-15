using HourGlassUnlimited.DataAccessLayer;
using HourGlassUnlimited.Models;
using HourGlassUnlimited.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HourGlassUnlimited.ViewModels
{
    public class SignUpVM : VM
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Confirmation { get; set; }
        public string Department { get; set; }

        public bool General_Status { get; set; }
        public bool Username_Status { get; set; }
        public bool Password_Status { get; set; }
        public bool Confirmation_Status { get; set; }

        public Visibility General_Feedback { get; set; }
        public Visibility Username_Feedback { get; set; }
        public Visibility Password_Feedback { get; set; }
        public Visibility Confirmation_Feedback { get; set; }

        public string General_Error { get; set; }
        public string Username_Error { get; set; }
        public string Password_Error { get; set; }
        public string Confirmation_Error { get; set; }

        public List<string> Departments { get; set; }

        public ICommand SignUp { get; set; }
        public ICommand GoBack { get; set; }

        public SignUpVM() : base()
        {
            General_Feedback = Visibility.Collapsed;
            Username_Feedback = Visibility.Collapsed;
            Password_Feedback = Visibility.Collapsed;
            Confirmation_Feedback = Visibility.Collapsed;

            Username_Error = "Nom d'utilisateur requis";
            Password_Error = "Mot de passe requis";
            Confirmation_Error = "Confirmation du mot de passe requise";

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
            Username_Status = Validate(Username);
            Password_Status = Validate(Password);
            Confirmation_Status = Validate(Confirmation);

            General_Status = (Password == Confirmation);

            if (General_Status && Username_Status && Password_Status && Confirmation_Status)
            {
                ProcessSignUp();
            }

            General_Error = "Le mot de passe et la confirmation doivent correspondent";
            DisplayFeedBack();
        }

        private bool GoBack_CanExecute(object parameter) { return true; }
        private void GoBack_Execute(object parameter)
        {
            Navigator.SignInView();
        }

        public bool Validate(string str)
        {
            if (string.IsNullOrWhiteSpace(str)) { return false; }

            return true;
        }

        private void ProcessSignUp()
        {
            string[] result = ConnectionHelper.SignUp(new User(0, Username, Password, Department));
            if (result[0] != "Success")
            {
                General_Status = false;
                AssignErrors(result);
            }
            else
            {
                Navigator.SignInView();
            }
        }

        private void DisplayFeedBack()
        {
            if (Username_Status) { Username_Feedback = Visibility.Collapsed; }
            else { Username_Feedback = Visibility.Visible; }
            ChangeValue("Username_Feedback");
            ChangeValue("Username_Error");

            if (Password_Status) { Password_Feedback = Visibility.Collapsed; }
            else { Password_Feedback = Visibility.Visible; }
            ChangeValue("Password_Feedback");
            ChangeValue("Password_Error");

            if (Confirmation_Status) { Confirmation_Feedback = Visibility.Collapsed; }
            else { Confirmation_Feedback = Visibility.Visible; }
            ChangeValue("Confirmation_Feedback");
            ChangeValue("Confirmation_Error");

            if (General_Status) { General_Feedback = Visibility.Collapsed; }
            else { General_Feedback = Visibility.Visible; }
            ChangeValue("General_Feedback");
            ChangeValue("General_Error");
        }

        private void AssignErrors(string[] result)
        {
            if (!General_Status)
            {
                if (result[0] == "Unauthorized")
                {
                    Username_Status = false;
                    Username_Error = result[1];
                }
                else
                {
                    General_Status = false;
                    General_Error = result[1];
                }
            }
        }
    }
}
