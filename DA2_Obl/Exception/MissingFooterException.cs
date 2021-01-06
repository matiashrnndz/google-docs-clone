using System;

namespace Exception
{
    [Serializable]
    public class MissingFooterException : System.Exception
    {
        public MissingFooterException(string message) : base(message)
        {

        }
    }
}