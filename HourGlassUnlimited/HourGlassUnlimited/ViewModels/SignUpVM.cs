using HourGlassUnlimited.DataAccessLayer;
using HourGlassUnlimited.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HourGlassUnlimited.ViewModels
{
    public class SignUpVM : VM
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Confirmation { get; set; }
        public string Department { get; set; }

        public List<string> Departments { get; set; }

        public SignUpVM() : base()
        {
            Departments = new List<string>();
            var temp = DAL.Departments.All();

            foreach (Department item in temp)
            {
                Departments.Add(item.Name);
            }

            Department = Departments[0];
        }
    }
}
