using System;

namespace Exception
{
    [Serializable]
    public class ExistingHeaderException : System.Exception
    {
        public ExistingHeaderException(string message) : base(message)
        {

        }
    }
}