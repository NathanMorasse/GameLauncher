using DatabaseManager.DataAccessLayer;
using DatabaseManager.Models;
using DatabaseManager.Tools;
using DatabaseManager.ViewModels.Base;
using DatabaseManager.ViewModels.Helpers;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.IO;
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
        public Department Edit { get; set; }

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
            string error = null;
            Department exist = DAL.Departments.ByName(Create.Name);

            if (exist.Id > 0)
            {
                error = "Le nom de département est déjà utilisé.";
            }
            else if (Create.Name == null || Create.Name == string.Empty)
            {
                error = "Un nom de département est requis.";
            }
            else if (Create.Building == null || Create.Building == string.Empty)
            {
                error = "La lettre d'un bâtiment est requise.";
            }
            else if (Create.Floor < 1 || Create.Floor > 6)
            {
                error = "Un numéro d'étage est requis. (Entre 1 et 6)";
            }

            if (error != null)
            {
                Navigator.DepartmentListView.ShowError(error);
            }
            else
            {
                DAL.Departments.Create(Create);

                Departments = DAL.Departments.All();

                ChangeValue("Departments");
            }
        }

        private void EditDepartment_Execute(object parameter)
        {
            string error = null;
            Department exist = DAL.Departments.ByName(Edit.Name);

            if (exist.Id > 0 && exist.Id != Edit.Id)
            {
                error = "Le nom de département est déjà utilisé.";
            }
            else if (Edit.Name == null || Edit.Name == string.Empty)
            {
                error = "Un nom de département est requis.";
            }
            else if (Edit.Building == null || Edit.Building == string.Empty)
            {
                error = "La lettre d'un bâtiment est requise.";
            }
            else if (Edit.Floor < 1 || Edit.Floor > 6)
            {
                error = "Un numéro d'étage est requis. (Entre 1 et 6)";
            }

            if (error != null)
            {
                Navigator.DepartmentListView.ShowError(error);
            }
            else
            {
                DAL.Departments.Update(Edit);

                Departments = DAL.Departments.All();

                ChangeValue("Departments");
            }
        }

        private void EditPopUp_Execute(object parameter)
        {
            Edit = DAL.Departments.ById(Selected.Id);
            ChangeValue("Edit");
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
