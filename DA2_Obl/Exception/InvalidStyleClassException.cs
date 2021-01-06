using System;

namespace Exception
{
    [Serializable]
    public class InvalidStyleClassException : System.Exception
    {
        public InvalidStyleClassException(string message) : base(message)
        {

        }
    }
}