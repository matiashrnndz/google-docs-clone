using System;

namespace Exception
{
    [Serializable]
    public class ExistingFooterException : System.Exception
    {
        public ExistingFooterException(string message) : base(message)
        {

        }
    }
}