using Domain;
using System;
using System.Collections.Generic;

namespace Repository
{
    public interface IFriendRequestRepository
    {
        IEnumerable<FriendRequest> GetAllReceivedByEmail(string userAEmail);
        IEnumerable<FriendRequest> GetAllAcceptedByEmail(string userEmail);
        void Add(FriendRequest friendRequest);
        void Delete(FriendRequest friendRequest);
        void Accept(Guid id);
        FriendRequest GetByEmails(string receiverEmail, string senderEmail);
        bool Exists(string senderEmail, string receiverEmail);
    }
}
