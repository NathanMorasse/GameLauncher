using HourGlassUnlimited.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HourGlassUnlimited.Models
{
    public class GameBase : ModelBase
    {
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public string Path { get; set; }

        public GameBase() { }
    }
}
