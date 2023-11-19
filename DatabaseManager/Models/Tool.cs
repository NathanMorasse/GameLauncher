using DatabaseManager.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager.Models
{
    public class Tool : ModelTemplate
    {
        public int Board_Id { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }


        public Tool(int id, int board, string brand, string type) : base(id)
        {
            Board_Id = board;
            Brand = brand;
            Type = type;
        }
    }
}
