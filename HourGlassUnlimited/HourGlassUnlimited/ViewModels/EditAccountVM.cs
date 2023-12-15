using HourGlassUnlimited.DataAccessLayer;
using HourGlassUnlimited.Models;
using HourGlassUnlimited.Tools;
using Mysqlx.Session;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Math.Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HourGlassUnlimited.ViewModels
{
    public class EditAccountVM : VM
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Confirmation { get; set; }
        public string Department { get; set; }
        public string Current { get; set; }

        public bool Username_Status { get; set; }
        public bool Current_Status { get; set; }
        public bool General_Status { get; set; }

        public Visibility Username_Feedback { get; set; }
        public Visibility Current_Feedback { get; set; }
        public Visibility General_Feedback { get; set; }

        public string Username_Error { get; set; }
        public string Current_Error { get; set; }
        public string General_Error { get; set; }

        public List<string> Departments { get; set; }

        public ICommand Edit { get; set; }

        public EditAccountVM() : base()
        {
            Username_Feedback = Visibility.Collapsed;
            Current_Feedback = Visibility.Collapsed;
            General_Feedback = Visibility.Collapsed;

            Username_Status = true;
            Current_Status = true;
            General_Status = true;

            Current_Error = "Mot de passe actuel requis";

            Edit = new CommandLink(Edit_Execute, Edit_CanExecute);

            LoadDepartments();
            LoadUser();
        }

        private void LoadDepartments()
        {
            Departments = new List<string>();
            DAL dal = new DAL();
            var temp = dal.Departments.All();

            foreach (Department item in temp)
            {
                Departments.Add(item.Name);
            }

            Department = Departments[0];
        }

        private void LoadUser()
        {
            DAL dal = new DAL();
            var user = dal.Users.ByUsername(ConnectionHelper.User.Username);
            Username = user.Username;
            Department = user.Department;
        }

        private bool Edit_CanExecute(object parameter) { return true; }
        private void Edit_Execute(object parameter)
        {
            if (string.IsNullOrWhiteSpace(Username)) { Username = null; }
            if (string.IsNullOrWhiteSpace(Password)) { Password = null; }
            if (string.IsNullOrWhiteSpace(Confirmation)) { Confirmation = null; }

            Current_Status = !(Current == null);

            if (Current_Status)
            {
                Current_Status = (ConnectionHelper.ValidateHashedPassword(Current, ConnectionHelper.User.Password));
                if (!Current_Status) { Current_Error = "Mot de passe invalide"; }
            }

            General_Status = (Password == Confirmation);

            if (!General_Status) { General_Error = "Le mot de passe et la confirmation doivent correspondent"; }

            if (Username == null) { Username = ConnectionHelper.User.Username; }
            if (Password == null) { Password = ConnectionHelper.User.Password; }

            if (Current_Status && General_Status)
            {
                ProcessEdit();
            }

            DisplayFeedBack();
        }

        public bool Validate(string str)
        {
            if (string.IsNullOrWhiteSpace(str)) { return false; }

            return true;
        }

        private void ProcessEdit()
        {
            DAL dal = new DAL();
            string[] result = dal.Users.Update(Copy());
            if (result[0] != "Success")
            {
                General_Status = false;
                AssignErrors(result);
                Reset();
            }
            else
            {
                Navigator.GameListView();
            }
        }

        private User Copy()
        {
            User u = ConnectionHelper.User;
            User user = new User(u.Id, Username, ConnectionHelper.HashPassword(Password), Department);

            return user;
        }

        private void DisplayFeedBack()
        {
            if (Username_Status) { Username_Feedback = Visibility.Collapsed; }
            else { Username_Feedback = Visibility.Visible; }
            ChangeValue("Username_Feedback");
            ChangeValue("Username_Error");

            if (Current_Status) { Current_Feedback = Visibility.Collapsed; }
            else { Current_Feedback = Visibility.Visible; }
            ChangeValue("Current_Feedback");
            ChangeValue("Current_Error");

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
                    General_Status = true;
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

        private void Reset()
        {
            Password = null;
            Confirmation = null;
        }
    }
}
