using DatabaseManager.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager.Models
{
    public class ElectricOutlet : ModelTemplate
    {
        public int Room_Id { get; set; }
        public string Location { get; set; }
        public ElectricOutlet(int id, int room_Id, string location) : base(id)
        {
            Room_Id = room_Id;
            Location = location;
        }
    }
}
