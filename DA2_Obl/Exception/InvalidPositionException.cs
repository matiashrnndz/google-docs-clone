using System;

namespace Exception
{
    [Serializable]
    public class InvalidPositionException : System.Exception
    {
        public InvalidPositionException(string message) : base(message)
        {

        }
    }
}