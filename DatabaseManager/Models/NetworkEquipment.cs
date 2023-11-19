using DatabaseManager.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager.Models
{
    public class NetworkEquipment : ModelTemplate
    {
        public int Room_Id { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Specifications { get; set; }

        public NetworkEquipment(int id, int room, string type, string brand, string model, string spec) : base(id)
        {
            Room_Id = room;
            Type = type;
            Brand = brand;
            Model = model;
            Specifications = spec;
        }
    }
}
