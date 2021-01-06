using System;

namespace Exception
{
    [Serializable]
    public class MissingHeaderException : System.Exception
    {
        public MissingHeaderException(string message) : base(message)
        {

        }
    }
}