using Domain;
using System.Collections.Generic;

namespace Service
{
    public interface IFriendRequestManagementService
    {
        IEnumerable<User> ListFriendsByEmail(string user_email);
        void AddFriendRequest(string senderEmail, string receiverEmail);
        void RespondFriendRequest(string receiverEmail, string senderEmail, bool answer);
        IEnumerable<User> ListReceivedFriendRequestsByEmail(string user_email);
        FriendRequest GetFriendRequest(string senderEmail, string receiverEmail);
    }
}
