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
            var temp = DAL.Departments.All();

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
            bool old_pwd;
            bool new_pwd;
            bool uname;

            old_pwd = ConnectionHelper.ValidateHashedPassword(Current, ConnectionHelper.User.Password);
            new_pwd = (Password != null && Confirmation != null);
            new_pwd = (Password == null && Confirmation == null);
            new_pwd = (ValidateString(Password) && ValidateString(Confirmation));
            new_pwd = (Password == Confirmation);
            uname = (Username != null);
            uname = (ValidateString(Username));

            if (Username == null || ValidateString(Username))
            {
                return;
            }

            if (Password != null && Confirmation != null)
            {

            }
        }

        public bool ValidateString(string str)
        {
            if (str == "" || str == " ") { return false; }
            if (str.Length !> 4) { return false; }

            return true;
        }
    }
}
