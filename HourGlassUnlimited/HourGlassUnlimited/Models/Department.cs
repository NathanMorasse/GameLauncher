using HourGlassUnlimited.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HourGlassUnlimited.Models
{
    public class Department : ModelBase
    {
        public string Name { get; set; }

        public Department(int id, string name) : base()
        {
            Id = id;
            Name = name;
        }
    }
}
