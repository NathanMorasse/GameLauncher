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
    public class FurnitureDetailVM : ViewModelTemplate
    {
        public Furniture Target { get; set; }

        public List<Room> RoomList { get; set; }
        public Furniture Editable { get; set; }

        public ICommand Edit { get; set; }
        public ICommand Delete { get; set; }
        public ICommand Rooms { get; set; }
        public ICommand EditView { get; set; }


        public FurnitureDetailVM() 
        {
            Target = Statics.TargetedFurniture;

            RoomList = DAL.Rooms.All();

            this.Edit = new CommandLink(Edit_Execute, Dummy_CanExecute);
            this.Delete = new CommandLink(Delete_Execute, Dummy_CanExecute);
            this.Rooms = new CommandLink(Rooms_Execute, Dummy_CanExecute);
            this.EditView = new CommandLink(EditView_Execute, Dummy_CanExecute);
        }

        private void Edit_Execute(object parameter)
        {
            string error = null;

            if (string.IsNullOrWhiteSpace(Editable.Brand) || Editable.Brand == "")
            {
                error = "Une marque est requise.";
            }
            else if (string.IsNullOrWhiteSpace(Editable.Type) || Editable.Type == "")
            {
                error = "Un type est requis.";
            }
            else if (Editable.Length < 1 || Editable.Height < 1 || Editable.Width < 1)
            {
                error = "Les dimensions sont requises";
            }
            else if (Editable.Room_Id == 0)
            {
                error = "Un local de référence est requis.";
            }

            if (error != null)
            {
                Navigator.FurnitureDetailView.ShowError(error);
            }
            else
            {
                DAL.Furnitures.Update(Editable);

                Target = DAL.Furnitures.ById(Target.Id);

                Navigator.FurnitureDetailView.Edit_Stuff_Button_Click();

                ChangeValue("Target");
            }
        }

        private void Delete_Execute(object parameter)
        {
            DAL.Furnitures.Delete(Target.Id);

            Navigator.FurnitureList();
        }

        private void Rooms_Execute(object parameter)
        {
            Navigator.FurnitureList();
        }

        private void EditView_Execute(object parameter)
        {
            Editable = DAL.Furnitures.ById(Target.Id);

            ChangeValue("Editable");
        }

    }
}
