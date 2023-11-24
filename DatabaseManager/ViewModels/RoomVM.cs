using DatabaseManager.DataAccessLayer;
using DatabaseManager.Models;
using DatabaseManager.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager.ViewModels
{
    public class RoomVM : ViewModelTemplate
    {
        public List<Room> Rooms { get; set; }
        public Room Selected { get; set; }
        public Room Create { get; set; }

        public RoomVM()
        {
            Rooms = DAL.Rooms.All();
        }
    }
}
