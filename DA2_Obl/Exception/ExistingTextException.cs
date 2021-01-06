using System;

namespace Exception
{
    public class ExistingTextException : System.Exception
    {
        public ExistingTextException(string message) : base(message)
        {

        }
    }
}
