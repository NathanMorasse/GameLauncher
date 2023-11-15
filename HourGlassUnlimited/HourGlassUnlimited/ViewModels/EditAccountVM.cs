using HourGlassUnlimited.DataAccessLayer;
using HourGlassUnlimited.Models;
using HourGlassUnlimited.Tools;
using Org.BouncyCastle.Math.Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public List<string> Departments { get; set; }

        public ICommand Edit { get; set; }

        public EditAccountVM() : base()
        {
            Edit = new CommandLink(Edit_Execute, Edit_CanExecute);

            LoadDepartments();
            LoadUsers();
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

        private void LoadUsers()
        {
            Username = ConnectionHelper.User.Username;
            Department = ConnectionHelper.User.Department;
        }

        private bool Edit_CanExecute(object parameter) { return true; }
        private void Edit_Execute(object parameter)
        {
            if (Username == "") { Username = null; }
            if (Password == "") { Password = null; }
            if (Confirmation == "") { Confirmation = null; }

            if (Current == null) { return; }

            if (!ConnectionHelper.ValidateHashedPassword(Current, ConnectionHelper.User.Password)) { return; }

            if (Password != Confirmation) { return; }

            if (Password != null && Confirmation != null)
            {
                if (!ValidateString(Password) || !ValidateString(Confirmation)) { return; }
            }

            if (Username != null)
            {
                if (!ValidateString(Username)) { return; }
            }

            if (Username == null) { Username = ConnectionHelper.User.Username; }
            if (Password == null) { Password = ConnectionHelper.User.Password; }

            ConnectionHelper.User.Username = Username;
            ConnectionHelper.User.Password = ConnectionHelper.HashPassword(Password);
            ConnectionHelper.User.Department = Department;
            DAL dal = new DAL();
            dal.Users.Update(ConnectionHelper.User);

            Navigator.GameListView();
        }

        public bool ValidateString(string str)
        {
            if (str == "" || str == " ") { return false; }
            if (str.Length < 4) { return false; }

            return true;
        }
    }
}
