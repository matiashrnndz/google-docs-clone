using System;

namespace Exception
{
    [Serializable]
    public class MissingParagraphException : System.Exception
    {
        public MissingParagraphException(string message) : base(message)
        {

        }
    }
}