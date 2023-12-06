using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HourGlassUnlimited.Exceptions
{
    public class BoardValidationException: Exception
    {
        public BoardValidationException() { }
        public BoardValidationException(string message) : base(message)
        {

        }

        public BoardValidationException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
