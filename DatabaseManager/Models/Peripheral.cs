using DatabaseManager.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager.Models
{
    public class Peripheral : ModelTemplate
    {
        public int Room_Id { get; set; }
        public int Computer_Id { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public bool IsWiFi { get; set; }
        public bool IsBluetooth { get; set; }
        public string Description { get; set; }



        public Peripheral(int id, int room, int computer, string type, string brand, string model, bool wifi, bool bluetooth, string description) : base(id)
        {
            Room_Id = room;
            Computer_Id = computer;
            Type = type;
            Brand = brand;
            Model = model;
            IsWiFi = wifi;
            IsBluetooth = bluetooth;
            Description = description;
        }
    }
}
