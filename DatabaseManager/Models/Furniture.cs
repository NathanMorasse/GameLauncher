using DatabaseManager.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager.Models
{
    public class Furniture : ModelTemplate
    {
        public int Room_Id { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public double Length { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public string Number { get; set; }

        public Furniture(int id, int room, string brand, string type, string description, double l, double h, double w, string number) : base(id)
        {
            Room_Id = room;
            Brand = brand;
            Type = type;
            Description = description;
            Length = l;
            Height = h;
            Width = w;
            Number = number;
        }

        public Furniture() { }
    }
}
