using System;

namespace Exception
{
    [Serializable]
    public class MissingContentException : System.Exception
    {
        public MissingContentException(string message) : base(message)
        {

        }
    }
}