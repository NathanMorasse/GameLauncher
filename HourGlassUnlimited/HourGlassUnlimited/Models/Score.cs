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
        public int UserId { get; set; }
        public int GameId { get; set; }
        public string Category { get; set; }
        public string Result { get; set; }
        public TimeSpan Time { get; set; }
        public int Points { get; set; }
        public DateTime Date { get; set; }

        public Score(int user, int game, string category, string result, TimeSpan time, int points, DateTime date) 
        {
            UserId = user;
            GameId = game;
            Category = category;
            Result = result;
            Time = time;
            Points = points;
            Date = date;
        }
    }
}
