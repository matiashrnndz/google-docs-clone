using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exception
{
    public class ExistingFormatException : System.Exception
    {
        public ExistingFormatException(string message) : base(message)
        {
        }
    }
}