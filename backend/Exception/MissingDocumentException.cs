using System;

namespace Exception
{
    public class MissingDocumentException : System.Exception
    {
        public MissingDocumentException(string message) : base(message)
        {
        }
    }
}
