using System;

namespace Exception
{
    [Serializable]
    public class InvalidRatingException : System.Exception
    {
        public InvalidRatingException(string message) : base(message)
        {
            message = "Invalid rating, must be from 1 to 5 stars.";
        }
    }
}