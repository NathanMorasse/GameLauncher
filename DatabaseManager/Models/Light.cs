using DatabaseManager.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager.Models
{
    public class Light : ModelTemplate
    {
        public int Room_Id { get; set; }
        public int LightSwitch_Id { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }

        public Light(int id, int room, int lightswitch, string brand, string type) : base(id)
        {
            Room_Id = room;
            LightSwitch_Id = lightswitch;
            Type = type;
            Brand = brand;
        }
    }
}
