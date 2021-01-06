using System;

namespace Exception
{
    public class MissingUserException : System.Exception
    {
        public MissingUserException(string message) : base(message)
        {

        }
    }
}
