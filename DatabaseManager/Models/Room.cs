using DatabaseManager.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager.Models
{
    public class Room : ModelTemplate
    {
        public string Department { get; set; }
        public string Number { get; set; }
        public bool HasAirConditioning { get; set; }
        public bool HasHeaters { get; set; }
        public bool HasPhone { get; set; }
        public bool HasMovementSensor { get; set; }

        public Room(int id, string department, string number, bool ac, bool heaters, bool phone, bool sensor) : base(id)
        {
            Department = department;
            Number = number;
            HasAirConditioning = ac;
            HasHeaters = heaters;
            HasPhone = phone;
            HasMovementSensor = sensor;
        }

        public Room()
        {

        }
    }
}
