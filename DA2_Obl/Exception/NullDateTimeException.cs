using System;

namespace Exception
{
    [Serializable]
    public class NullDateTimeException : System.Exception
    {
        public NullDateTimeException(string message) : base(message)
        {
        }
    }
}