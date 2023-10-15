using HourGlassUnlimited.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HourGlassUnlimited.Models
{
    public class Score : ModelBase
    {
        public int PlayerId { get; set; }
        public int GameId { get; set; }
        public int? Points { get; set; }
        public string? Result { get; set; }
        public string? Mode { get; set; }
        public TimeSpan? Time { get; set; }
        public DateTime Date { get; set; }

        public Score() { }
    }
}
