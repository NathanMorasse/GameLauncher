using DatabaseManager.DataAccessLayer;
using DatabaseManager.Models;
using DatabaseManager.Tools;
using DatabaseManager.ViewModels.Base;
using DatabaseManager.ViewModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace DatabaseManager.ViewModels
{
    public class RoomDetailVM : ViewModelTemplate
    {
        public Room Target { get; set; }
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

        public ICommand AllRooms { get; set; }
        public ICommand EditView { get; set; }
        public ICommand Edit { get; set; }
        public ICommand Delete { get; set; }

        public RoomDetailVM()
        {
            Target = Statics.TargetedRoom;
            Statics.TargetedRoom = null;

            Departments = new List<string>();

            foreach (Department item in DAL.Departments.All())
            {
                Departments.Add(item.Name);
            }

            this.AllRooms = new CommandLink(AllRooms_Execute, Dummy_CanExecute);
            this.EditView = new CommandLink(EditView_Execute, Dummy_CanExecute);
            this.Edit = new CommandLink(Edit_Execute, Dummy_CanExecute);
            this.Delete = new CommandLink(Delete_Execute, Dummy_CanExecute);
        }

        private void AllRooms_Execute(object parameter)
        {
            Navigator.RoomList();
        }

        private void EditView_Execute(object parameter)
        {
            RawNumber = Target.Number;

            ChangeValue("RawNumber");
        }

        private void Edit_Execute(object parameter)
        {
            int convert = int.Parse(RawNumber);

            DAL.Rooms.Update(Target, convert);
            Target = DAL.Rooms.ById(Target.Id);
            RawNumber = Target.Number;

            ChangeValue("Target");
            ChangeValue("RawNumber");
        }

        private void Delete_Execute(object parameter)
        {
            DAL.Rooms.Delete(Target.Id);

            Navigator.RoomList();
        }
    }
}
