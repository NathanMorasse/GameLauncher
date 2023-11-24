using DatabaseManager.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager.Models
{
    public class Department : ModelTemplate
    {
        public string Name { get; set; }
        public string Building { get; set; }
        public int Floor { get; set; }

        public Department(int id, string name, string building, int floor) : base(id)
        {
            Name = name;
            Building = building;
            Floor = floor;
        }

        public Department() { }
    }
}
