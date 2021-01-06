using System;

namespace Domain
{
    public class FriendRequest
    {
        public Guid Id { get; set; }
        public User Sender { get; set; }
        public User Receiver { get; set; }
        public bool Accepted { get; set; }

        public FriendRequest()
        {
            Sender = null;
            Receiver = null;
            Accepted = false;
        }
    }
}