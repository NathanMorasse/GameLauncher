using DatabaseManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager.ViewModels.Helpers
{
    public static class Statics
    {
        public static Room TargetedRoom { get; set; }
        public static Department TargetedDepartment { get; set; }
        public static Furniture TargetedFurniture { get; set; }
    }
}
