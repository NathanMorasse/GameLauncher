using HourGlassUnlimited.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HourGlassUnlimited.Models
{
    public class User : ModelBase
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Departement { get; set; }
    }
}
