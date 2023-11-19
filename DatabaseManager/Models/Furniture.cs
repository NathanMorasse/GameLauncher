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
        int Room_Id { get; set; }
        string Brand { get; set; }
        string Type { get; set; }
        string Descritpion { get; set; }
        double Length { get; set; }
        double Height { get; set; }
        double Width { get; set; }

        public Furniture(int id, int room, string brand, string type, string description, double l, double h, double w) : base(id)
        {
            Room_Id = room;
            Brand = brand;
            Type = type;
            Descritpion = description;
            Length = l;
            Height = h;
            Width = w;
        }
    }
}
