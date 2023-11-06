using HourGlassUnlimited.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HourGlassUnlimited.ViewModels
{
    public class RankingsVM : VM
    {
        public List<RankingSection> Sections { get; set; }

        public RankingsVM() 
        {

        }
    }
}
