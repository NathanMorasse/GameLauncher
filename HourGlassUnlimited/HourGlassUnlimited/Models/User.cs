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
        public string Username { get; set; }
        public string Password { get; set; }
        public string Department { get; set; }

        public User(int id, string username, string password, string department) : base()
        {
            Id = id;
            Username = username;
            Password = password;
            Department = department;
        }
    }
}
