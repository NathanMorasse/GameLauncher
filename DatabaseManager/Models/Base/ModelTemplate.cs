using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager.Models.Base
{
    public class ModelTemplate
    {
        public int Id { get; set; }

        public ModelTemplate(int id)
        {
            Id = id;
        }
    }
}
