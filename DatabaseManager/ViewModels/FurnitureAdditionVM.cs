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
    public class FurnitureAdditionVM : ViewModelTemplate
    {
        public Furniture Target { get; set; }
        public List<Room> RoomList { get; set; }

        public ICommand Create { get; set; }
        public ICommand Back { get; set; }


        public FurnitureAdditionVM()
        {
            Target = new Furniture();

            RoomList = DAL.Rooms.All();

            this.Create = new CommandLink(Create_Execute, Dummy_CanExecute);
            this.Back = new CommandLink(Back_Execute, Dummy_CanExecute);
        }

        private void Create_Execute(object parameter)
        {
            string error = null;

            if (string.IsNullOrWhiteSpace(Target.Brand) || Target.Brand == "")
            {
                error = "Une marque est requise.";
            }
            else if(string.IsNullOrWhiteSpace(Target.Type) || Target.Type == "")
            {
                error = "Un type est requise.";
            }
            else if (Target.Length == 0 || Target.Height == 0 || Target.Width == 0)
            {
                error = "Les dimensions sont requises.";
            }
            else if (Target.Room_Id == 0)
            {
                error = "Un local de référence est requis.";
            }

            if (error != null)
            {
                Navigator.FurnitureAdditionView.ShowError(error);
            }
            else
            {
                DAL.Furnitures.Create(Target);

                Navigator.FurnitureList();
            }
        }

        private void Back_Execute(object parameter)
        {
            Navigator.FurnitureList();
        }
    }
}
