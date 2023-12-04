using DatabaseManager.DataAccessLayer;
using DatabaseManager.Models;
using DatabaseManager.Tools;
using DatabaseManager.ViewModels.Base;
using DatabaseManager.ViewModels.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DatabaseManager.ViewModels
{
    public class DepartmentRoomListVM : ViewModelTemplate
    {
        public Department Target { get; set; }
        public List<Room> Rooms { get; set; }
        public Room Selected { get; set; }
        public Room Create { get; set; }
        public Room Edit { get; set; }

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
                if (value.Length > 4)
                    _RawNumber = value.Substring(2);
                else
                    _RawNumber = value;
            }
        }

        public ICommand EditPopUp { get; set; }
        public ICommand NewRoom { get; set; }
        public ICommand DeleteRoom { get; set; }
        public ICommand EditRoom { get; set; }
        public ICommand SeeRoom { get; set; }

        public DepartmentRoomListVM()
        {
            Target = Statics.TargetedDepartment;
            Create = new Room();
            Create.Department = Target.Name;
            Rooms = DAL.Rooms.ByDepartment(Target.Id);

            List<Department> temp = DAL.Departments.All();
            Departments = new List<string>();

            foreach (Department item in temp)
            {
                Departments.Add(item.Name);
            }

            this.EditPopUp = new CommandLink(EditPopUp_Execute, Dummy_CanExecute);
            this.NewRoom = new CommandLink(NewRoom_Execute, Dummy_CanExecute);
            this.DeleteRoom = new CommandLink(DeleteRoom_Execute, Dummy_CanExecute);
            this.EditRoom = new CommandLink(EditRoom_Execute, Dummy_CanExecute);
            this.SeeRoom = new CommandLink(SeeRoom_Execute, Dummy_CanExecute);
        }

        private void EditPopUp_Execute(object parameter)
        {
            RawNumber = Selected.Number;
            Edit = DAL.Rooms.ById(Selected.Id);

            ChangeValue("Edit");
            ChangeValue("Selected");
            ChangeValue("RawNumber");
        }

        private void NewRoom_Execute(object parameter)
        {
            string error = null;

            if (Create.Number == null)
            {
                error = "Un numéro à 4 chiffres est requis.";
            }
            else if (Create.Number.Length != 4)
            {
                error = "Un numéro à 4 chiffres est requis.";
            }

            if (error != null)
            {
                Navigator.DepartmentRoomListView.ShowError(error);
            }
            else
            {
                DAL.Rooms.Create(Create);

                Rooms = DAL.Rooms.ByDepartment(Target.Id);
                ChangeValue("Rooms");
            }
        }

        private void DeleteRoom_Execute(object parameter)
        {
            DAL.Rooms.Delete(Selected.Id);

            Rooms = DAL.Rooms.ByDepartment(Target.Id);
            ChangeValue("Rooms");
        }

        private void EditRoom_Execute(object parameter)
        {
            string error = null;

            if (RawNumber == null)
            {
                error = "Un numéro à 4 chiffres est requis.";
            }
            else if (RawNumber.Length != 4)
            {
                error = "Un numéro à 4 chiffres est requis.";
            }

            if (error != null)
            {
                Navigator.DepartmentRoomListView.ShowError(error);
            }
            else
            {
                int convert = int.Parse(RawNumber);

                DAL.Rooms.Update(Edit, convert);

                Rooms = DAL.Rooms.ByDepartment(Target.Id);
                ChangeValue("Rooms");
            }
        }

        private void SeeRoom_Execute(object parameter)
        {
            Statics.TargetedRoom = Selected;
            Navigator.RoomDetail();
        }
    }
}
