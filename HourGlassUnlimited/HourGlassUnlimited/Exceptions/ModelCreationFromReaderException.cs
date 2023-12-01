using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HourGlassUnlimited.Exceptions
{
    public class ModelCreationFromReaderException : Exception
    {
        public ModelCreationFromReaderException() { }
        public ModelCreationFromReaderException(string message) : base(message) 
        {

        }

        public ModelCreationFromReaderException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
