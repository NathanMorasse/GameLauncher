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

        public ICommand Edit { get; set; }
        public ICommand Delete { get; set; }
        public ICommand Rooms { get; set; }


        public FurnitureDetailVM() 
        {
            Target = Statics.TargetedFurniture;

            RoomList = DAL.Rooms.All();

            this.Edit = new CommandLink(Edit_Execute, Dummy_CanExecute);
            this.Delete = new CommandLink(Delete_Execute, Dummy_CanExecute);
            this.Rooms = new CommandLink(Rooms_Execute, Dummy_CanExecute);
        }

        private void Edit_Execute(object parameter)
        {
            DAL.Furnitures.Update(Target);

            Target = DAL.Furnitures.ById(Target.Id);

            ChangeValue("Target");
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

    }
}
