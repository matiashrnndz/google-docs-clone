using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exception
{
    public class MissingStyleClassException : System.Exception
    {
        public MissingStyleClassException(string message) : base(message)
        {

        }
    }
}
