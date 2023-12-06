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
    public class SignInVM : VM
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public bool General_Status { get; set; }
        public bool Username_Status { get; set; }
        public bool Password_Status { get; set; }

        public Visibility General_Feedback { get; set; }
        public Visibility Username_Feedback { get; set; }
        public Visibility Password_Feedback { get; set; }

        public string General_Error { get; set; }
        public string Username_Error { get; set; }
        public string Password_Error { get; set; }

        public ICommand SignIn { get; set; }
        public ICommand GoToSignUp { get; set; }

        public SignInVM()
        {
            Username_Error = "Nom d'utilisateur requis";
            Password_Error = "Mot de passe requis";

            General_Status = true;
            Username_Status = true;
            Password_Status = true;

            General_Feedback = Visibility.Collapsed;
            Username_Feedback = Visibility.Collapsed;
            Password_Feedback = Visibility.Collapsed;

            SignIn = new CommandLink(SignIn_Execute, SignIn_CanExecute);
            GoToSignUp = new CommandLink(GoToSignUp_Execute, GoToSignUp_CanExecute);
        }

        private bool SignIn_CanExecute(object parameter) { return true; }
        private void SignIn_Execute(object parameter) 
        {
            Username_Status = Validate(Username);
            Password_Status = Validate(Password);
            General_Status = true;

            ProcessSignIn();

            DisplayFeedBack();
        }

        private bool GoToSignUp_CanExecute(object parameter) { return true; }
        private void GoToSignUp_Execute(object parameter) { Navigator.SignUpView(); }

        private bool Validate(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return false;

            return true;
        }

        private void AssignErrors(string code)
        {
            if (!General_Status)
            {
                switch (code)
                {
                    case "Error": { General_Error = "Une erreur est survenue, veuillez réessayer"; break; }
                    case "Unauthorized": { General_Error = "Les informations fournis ne sont pas valide"; break; }
                    case "Wrong": { General_Error = "Le compte n'a pas été trouvé"; break; }
                    default: { General_Error = "Une erreur est survenue, veuillez réessayer"; break; }
                }
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

            if (General_Status) { General_Feedback = Visibility.Collapsed; }
            else { General_Feedback = Visibility.Visible; }
            ChangeValue("General_Feedback");
            ChangeValue("General_Error");
        }

        private void ProcessSignIn()
        {
            string result;
            if (Username_Status == true && Password_Status == true)
            {
                result = ConnectionHelper.SignIn(Username, Password);
                if (result != "Success")
                {
                    General_Status = false;
                    AssignErrors(result);
                }
                else
                {
                    Navigator.GameListView();
                }
            }
        }
    }
}
