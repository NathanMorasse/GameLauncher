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
    public class RoomDetailVM : ViewModelTemplate
    {
        public ICommand AllRooms { get; set; }

        public RoomDetailVM()
        {
            this.AllRooms = new CommandLink(AllRooms_Execute, Dummy_CanExecute);
        }

        private void AllRooms_Execute(object parameter)
        {
            Navigator.RoomList();
        }
    }
}
