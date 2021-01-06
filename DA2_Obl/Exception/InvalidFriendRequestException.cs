using System;
using System.Runtime.Serialization;

namespace Exception
{
    [Serializable]
    public class InvalidFriendRequestException : System.Exception
    {
        public InvalidFriendRequestException(string message) : base(message)
        {

        }
    }
}