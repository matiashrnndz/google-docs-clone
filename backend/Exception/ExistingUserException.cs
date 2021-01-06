using System;

namespace Exception
{
    public class ExistingUserException : System.Exception
    {
        public ExistingUserException(string message) : base(message)
        {

        }
    }
}
