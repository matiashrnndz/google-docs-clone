using System;

namespace Exception
{
    [Serializable]
    public class MissingTextException : System.Exception
    {
        public MissingTextException(string message) : base(message)
        {

        }
    }
}