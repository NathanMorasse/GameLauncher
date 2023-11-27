using DatabaseManager.DataAccessLayer;
using DatabaseManager.Models;
using DatabaseManager.Tools;
using DatabaseManager.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DatabaseManager.ViewModels
{
    public class RoomVM : ViewModelTemplate
    {
        public List<Room> Rooms { get; set; }
        public Room Selected { get; set; }
        public Room Create { get; set; }

        public List<string> Departments { get; set; }

        private string _RawNumber;
        public string RawNumber 
        { 
            get
            {
                return _RawNumber;
            }

            set
            {
                _RawNumber = value.Substring(2);
            } 
        }

        public ICommand EditPopUp { get; set; }

        public RoomVM()
        {
            Rooms = DAL.Rooms.All();

            List<Department> temp = DAL.Departments.All();
            Departments = new List<string>();

            foreach (Department item in temp)
            {
                Departments.Add(item.Name);
            }

            this.EditPopUp = new CommandLink(EditPopUp_Execute, Dummy_CanExecute);
        }

        private void EditPopUp_Execute(object parameter)
        {
            RawNumber = Selected.Number;

            ChangeValue("Selected");
            ChangeValue("RawNumber");
        }
    }
}
