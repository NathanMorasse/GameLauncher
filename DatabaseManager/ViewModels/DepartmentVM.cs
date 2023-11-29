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
using System.Windows.Input;

namespace DatabaseManager.ViewModels
{
    public class DepartmentVM : ViewModelTemplate
    {
        public List<Department> Departments { get; set; }
        public Department Selected { get; set; }
        public Department Create { get; set; }

        public ICommand AllDepartment {  get; set; }
        public ICommand NewDepartment { get; set; }
        public ICommand EditDepartment { get; set; }
        public ICommand EditPopUp { get; set; }
        public ICommand DeleteDepartment { get; set; }
        public ICommand SeeDepartmentRooms { get; set; }

        public DepartmentVM() 
        {
            Departments = DAL.Departments.All();
            Create = new Department();

            this.AllDepartment = new CommandLink(AllDepartment_Execute, Dummy_CanExecute);
            this.NewDepartment = new CommandLink(NewDepartment_Execute, Dummy_CanExecute);
            this.EditDepartment = new CommandLink(EditDepartment_Execute, Dummy_CanExecute);
            this.EditPopUp = new CommandLink(EditPopUp_Execute, Dummy_CanExecute);
            this.DeleteDepartment = new CommandLink(DeleteDepartment_Execute, Dummy_CanExecute);
            this.SeeDepartmentRooms = new CommandLink(SeeDepartmentRooms_Execute, Dummy_CanExecute);
        }

        private void AllDepartment_Execute(object parameter)
        {
            Navigator.DepartmentList();
        }

        private void NewDepartment_Execute(object parameter)
        {
            DAL.Departments.Create(Create);

            Departments = DAL.Departments.All();

            ChangeValue("Departments");
        }

        private void EditDepartment_Execute(object parameter)
        {
            DAL.Departments.Update(Selected);

            Departments = DAL.Departments.All();

            ChangeValue("Departments");
        }

        private void EditPopUp_Execute(object parameter)
        {
            ChangeValue("Selected");
        }

        private void DeleteDepartment_Execute(object parameter)
        {
            DAL.Departments.Delete(Selected.Id);

            Departments = DAL.Departments.All();

            ChangeValue("Departments");
        }

        private void SeeDepartmentRooms_Execute(object parameter)
        {
            Statics.TargetedDepartment = Selected;
            Navigator.DepartmentRoomList();
        }
    }
}
