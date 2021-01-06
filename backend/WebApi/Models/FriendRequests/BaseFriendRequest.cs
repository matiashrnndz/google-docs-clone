using Domain;
using System;

namespace WebApi.Models.FriendRequests
{
    public class BaseFriendRequest : Model<FriendRequest, BaseFriendRequest>
    {
        public Guid Id { get; set; }
        public bool Accepted { get; set; }
        public User Sender { get; set; }
        public User Receiver { get; set; }

        public BaseFriendRequest()
        {

        }

        public BaseFriendRequest(FriendRequest friendRequest)
        {
            SetModel(friendRequest);
        }

        public override FriendRequest ToEntity() => new FriendRequest()
        {
            Id = this.Id,
            Sender = this.Sender,
            Receiver = this.Receiver,
            Accepted = this.Accepted
        };

        protected override BaseFriendRequest SetModel(FriendRequest friendRequest)
        {
            Id = friendRequest.Id;
            Sender = friendRequest.Sender;
            Receiver = friendRequest.Receiver;
            Accepted = friendRequest.Accepted;

            return this;
        }
    }
}