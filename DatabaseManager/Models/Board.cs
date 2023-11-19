using DatabaseManager.Models.Base;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager.Models
{
    public class Board : ModelTemplate
    {
        public int Room_Id { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }


        public Board(int id, int room, string type, string brand, double h, double w) : base(id)
        {
            Room_Id = room;
            Type = type;
            Brand = brand;
            Height = h;
            Width = w;
        }
    }
}
