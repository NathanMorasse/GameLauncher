using DatabaseManager.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager.Models
{
    public class Period : ModelTemplate
    {
        public int Room_Id { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public string Day { get; set; }
        public string TeacherName { get; set; }
        public string Class { get; set; }
        public string Group { get; set; }

        public Period(int id, int room, TimeSpan start, TimeSpan end, string day, string teacher, string className, string group) : base(id)
        {
            Room_Id = room;
            Start = start;
            End = end;
            Day = day;
            TeacherName = teacher;
            Class = className;
            Group = group;
        }
    }
}
