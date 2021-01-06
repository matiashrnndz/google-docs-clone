using System;

namespace Exception
{
    [Serializable]
    public class InvalidStyleException : System.Exception
    {
        public InvalidStyleException(string message) : base(message)
        {

        }
    }
}