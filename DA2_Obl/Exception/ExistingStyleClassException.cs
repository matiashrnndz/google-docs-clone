using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exception
{
    public class ExistingStyleClassException : System.Exception
    {
        public ExistingStyleClassException(string message) : base(message)
        {
        }
    }
}
