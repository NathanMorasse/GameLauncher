using DatabaseManager.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager.Models
{
    public class Display : ModelTemplate
    {
        public int Room_Id { get; set; }
        public int Computer_Id { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Resolution { get; set; }

        public Display(int id, int room, int computer, string type, string brand, string model, string resolution) : base(id)
        {
            Room_Id = room;
            Computer_Id = computer;
            Type = type;
            Brand = brand;
            Model = model;
            Resolution = resolution;
        }
    }
}
