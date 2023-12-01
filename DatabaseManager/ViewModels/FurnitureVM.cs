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
    public class FurnitureVM : ViewModelTemplate
    {
        public List<Furniture> Furnitures { get; set; }
        public Furniture Selected { get; set; }


        public ICommand See { get; set; }
        public ICommand Create { get; set; }
        public ICommand Delete { get; set; }


        public FurnitureVM() 
        {
            Furnitures = DAL.Furnitures.All();

            this.See = new CommandLink(See_Execute, Dummy_CanExecute);
            this.Create = new CommandLink(Create_Execute, Dummy_CanExecute);
            this.Delete = new CommandLink(Delete_Execute, Dummy_CanExecute);
        }


        public void See_Execute(object parameter)
        {
            Statics.TargetedFurniture = Selected;

            Navigator.FurnitureDetail();
        }

        public void Create_Execute(object parameter)
        {
            Statics.TargetedFurniture = Selected;

            Navigator.FurnitureAddition();
        }

        public void Delete_Execute(object parameter)
        {
            DAL.Furnitures.Delete(Selected.Id);
        }
    }
}
