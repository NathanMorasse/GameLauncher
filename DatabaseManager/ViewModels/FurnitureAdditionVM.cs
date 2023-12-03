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
            DAL.Furnitures.Create(Target);

            Navigator.FurnitureList();
        }

        private void Back_Execute(object parameter)
        {
            Navigator.FurnitureList();
        }
    }
}
