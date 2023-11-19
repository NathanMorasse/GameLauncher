using DatabaseManager.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager.Models
{
    public class Computer : ModelTemplate
    {
        public int Room_Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string OperatingSystem { get; set; }


        public Computer(int id, int room, string brand, string model, string os) : base(id)
        {
            Room_Id = room;
            Brand = brand;
            Model = model;
            OperatingSystem = os;
        }
    }
}
