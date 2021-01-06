using System;

namespace Exception
{
    public class MissingStyleException : System.Exception
    {
        public MissingStyleException(string message) : base(message)
        {

        }
    }
}
